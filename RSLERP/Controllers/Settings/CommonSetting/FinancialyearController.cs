using RSLERP.DataManager;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RSLERP.DataManager.Utility;

namespace RSLERP.Controllers.Settings
{
    public class FinancialyearController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Financialyear list
        /// </summary>
        /// <returns></returns>
        // GET: Financialyear
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_FINANCIALYEARS = new DBContext().Financialyears.ToList();


            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Financialyear 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            // int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";


            int fID = Convert.ToInt32(id);


            //pass model to view
            Financialyear mdlFinancialyear = new Financialyear();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Financialyear already exist
                if (new DBContext().Financialyears.ToList().FindAll(x => x.id == fID).Count > 0)
                {
                    //pass model to view with Financialyear info
                    mdlFinancialyear = new DBContext().Financialyears.Find(fID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_FINANCIALYEAR = mdlFinancialyear;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store Financialyear 
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().Financialyears.ToList().FindAll(x => x.id == vmdl.VM_FINANCIALYEAR.id).Count > 0)
                {
                    //Update Financialyear
                    //VM_FINANCIALYEAR.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Financialyears.Attach(vmdl.VM_FINANCIALYEAR);
                        contxt.Entry(vmdl.VM_FINANCIALYEAR).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Financialyear
                    using (var contxt = new DBContext())
                    {

                        contxt.Financialyears.Add(vmdl.VM_FINANCIALYEAR);
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index", vmdl);
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errorstate.errors(ModelState), ModelState);
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("load");
            }

        }



    }
}