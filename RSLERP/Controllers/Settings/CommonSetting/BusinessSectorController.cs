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

namespace RSLERP.Controllers.Settings.CommonSetting
{
    public class BusinessSectorController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Business Sector list
        /// </summary>
        /// <returns></returns>
        // GET: BusinessSector
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            vmdl.VM_BUSINESS_SECTORS = new DBContext().BusinessSectors.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Business Sector 
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

            int bsID = Convert.ToInt32(id);

            //pass model to view
            BusinessSector mdlBusinessSector = new BusinessSector();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Business Sector already exist
                if (new DBContext().BusinessSectors.ToList().FindAll(x => x.id == bsID).Count > 0)
                {
                    //pass model to view with Business Sector info
                    mdlBusinessSector = new DBContext().BusinessSectors.Find(bsID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_BUSINESS_SECTOR = mdlBusinessSector;
            }

            return View(vmdl);
        }


        /// <summary>
        /// Store Business Sector 
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
                if (new DBContext().BusinessSectors.ToList().FindAll(x => x.id == vmdl.VM_BUSINESS_SECTOR.id).Count > 0)
                {
                    //Update Business Sector
                    //VM_BUSINESS_SECTOR.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.BusinessSectors.Attach(vmdl.VM_BUSINESS_SECTOR);
                        contxt.Entry(vmdl.VM_BUSINESS_SECTOR).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Business Sector
                    using (var contxt = new DBContext())
                    {

                        contxt.BusinessSectors.Add(vmdl.VM_BUSINESS_SECTOR);
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