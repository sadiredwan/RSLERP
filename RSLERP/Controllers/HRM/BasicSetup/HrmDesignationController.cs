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
    public class HrmDesignationController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmDesignation list
        /// </summary>
        /// <returns></returns>
        // GET: HrmDesignation
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_DESIGNATIONS = new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmDesignation
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

            int dID = Convert.ToInt32(id);

            //pass model to view
            HrmDesignation mdlHrmDesignation = new HrmDesignation();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmDesignation already exist
                if (new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == dID).Count > 0)
                {
                    //pass model to view with HrmDesignation info
                    mdlHrmDesignation = new DBContext().HrmDesignations.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_DESIGNATION = mdlHrmDesignation;
            }

            vmdl.VM_HRM_DESIGNATIONS = new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmDesignation 
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
                if (new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_DESIGNATION.id).Count > 0)
                {
                    //Update HrmDesignation
                    //VM_HRM_DESIGNATION.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmDesignations.Attach(vmdl.VM_HRM_DESIGNATION);
                        contxt.Entry(vmdl.VM_HRM_DESIGNATION).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmDesignation
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmDesignations.Add(vmdl.VM_HRM_DESIGNATION);
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