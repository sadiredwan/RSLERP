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
    public class HrmSectionController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmSection list
        /// </summary>
        /// <returns></returns>
        // GET: HrmSection
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_SECTIONS = new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_DEPARTMENTS = new DBContext().Departments.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmSection
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

            int sID = Convert.ToInt32(id);

            //pass model to view
            HrmSection mdlHrmSection = new HrmSection();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmSection already exist
                if (new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == sID).Count > 0)
                {
                    //pass model to view with HrmSection info
                    mdlHrmSection = new DBContext().HrmSections.Find(sID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_SECTION = mdlHrmSection;
            }

            vmdl.VM_DEPARTMENTS = new DBContext().Departments.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmSection 
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
                if (new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_SECTION.id).Count > 0)
                {
                    //Update HrmSection
                    //VM_HRM_SECTION.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmSections.Attach(vmdl.VM_HRM_SECTION);
                        contxt.Entry(vmdl.VM_HRM_SECTION).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmSection
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmSections.Add(vmdl.VM_HRM_SECTION);
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