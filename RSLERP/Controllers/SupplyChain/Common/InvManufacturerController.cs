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
    public class InvManufacturerController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvManufacturer list
        /// </summary>
        /// <returns></returns>
        // GET: InvManufacturer
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_INVMANUFACTURERS = new DBContext().InvManufacturers.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvManufacturer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            // int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            //Set  Message
            String message = "";

            int mID = Convert.ToInt32(id);

            //pass model to view
            InvManufacturer mdlInvManufacturer = new InvManufacturer();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvManufacturer already exist
                if (new DBContext().InvManufacturers.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == mID).Count > 0)
                {
                    //pass model to view with InvManufacturer info
                    mdlInvManufacturer = new DBContext().InvManufacturers.Find(mID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVMANUFACTURER = mdlInvManufacturer;
            }
            vmdl.VM_COUNTRIES = new DBContext().Countries.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store InvManufacturer 
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {

            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().InvManufacturers.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVMANUFACTURER.id).Count > 0)
                {
                    //Update InvManufacturer
                    //VM_INVMANUFACTURER.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvManufacturers.Attach(vmdl.VM_INVMANUFACTURER);
                        contxt.Entry(vmdl.VM_INVMANUFACTURER).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvManufacturer
                    using (var contxt = new DBContext())
                    {

                        contxt.InvManufacturers.Add(vmdl.VM_INVMANUFACTURER);
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