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
    public class CountryController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Country list
        /// </summary>
        /// <returns></returns>
        // GET: Country
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            vmdl.VM_COUNTRIES = new DBContext().Countries.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Country 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            //int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";

            int cnID = Convert.ToInt32(id);

            //pass model to view
            Country mdlCountry = new Country();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Country already exist
                if (new DBContext().Countries.ToList().FindAll(x => x.id == cnID).Count > 0)
                {
                    //pass model to view with Country info
                    mdlCountry = new DBContext().Countries.Find(cnID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_COUNTRY = mdlCountry;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store Country 
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
                if (new DBContext().Countries.ToList().FindAll(x => x.id == vmdl.VM_COUNTRY.id).Count > 0)
                {
                    //Update Country
                    //VM_COUNTRY.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Countries.Attach(vmdl.VM_COUNTRY);
                        contxt.Entry(vmdl.VM_COUNTRY).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Country
                    using (var contxt = new DBContext())
                    {

                        contxt.Countries.Add(vmdl.VM_COUNTRY);
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