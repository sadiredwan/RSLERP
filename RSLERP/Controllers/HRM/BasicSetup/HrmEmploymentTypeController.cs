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
    public class HrmEmploymentTypeController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmEmploymentType list
        /// </summary>
        /// <returns></returns>
        // GET: HrmEmploymentType
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_EMPLOYMENT_TYPES = new DBContext().HrmEmploymentTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_TYPES = new DBContext().HrmEmployeeTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmEmploymentType
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

            int etID = Convert.ToInt32(id);

            //pass model to view
            HrmEmploymentType mdlHrmEmploymentType = new HrmEmploymentType();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmEmploymentType already exist
                if (new DBContext().HrmEmploymentTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == etID).Count > 0)
                {
                    //pass model to view with HrmEmploymentType info
                    mdlHrmEmploymentType = new DBContext().HrmEmploymentTypes.Find(etID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_EMPLOYMENT_TYPE = mdlHrmEmploymentType;
            }

            vmdl.VM_HRM_EMPLOYEE_TYPES = new DBContext().HrmEmployeeTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmEmploymentType 
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
                if (new DBContext().HrmEmploymentTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_EMPLOYMENT_TYPE.id).Count > 0)
                {
                    //Update HrmEmploymentType
                    //VM_HRM_EMPLOYMENT_TYPE.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmEmploymentTypes.Attach(vmdl.VM_HRM_EMPLOYMENT_TYPE);
                        contxt.Entry(vmdl.VM_HRM_EMPLOYMENT_TYPE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmEmploymentType
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmEmploymentTypes.Add(vmdl.VM_HRM_EMPLOYMENT_TYPE);
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