using RSLERP.Authorization;
using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Global;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static RSLERP.DataManager.Utility;

namespace RSLERP.Controllers.HRM.EmployeeManagement
{
    public class HrmEmployeeExperienceController : Controller
    {
        ViewModel vmdl = new ViewModel();

        // GET: HrmEmployeeExperiences
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.View })]
        public ActionResult Index()
        {
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            vmdl.VM_HRM_EMPLOYEE_EXPERIENCE = new HrmEmployeeExperience();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }

            vmdl.VM_HRM_EMPLOYEE_EXPERIENCES = new DBContext().HrmEmployeeExperiences.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store  
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.Create })]
        public ActionResult store(ViewModel vmdl)
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            vmdl.VM_HRM_EMPLOYEE_EXPERIENCES = new List<HrmEmployeeExperience>();

            if (Request.Form.GetValues("company_name[]") != null)
            {
                int totalExperiences = Request.Form.GetValues("company_name[]").Count();
                for (int i = 0; i < totalExperiences; i++)
                {
                    HrmEmployeeExperience hrmEx = new HrmEmployeeExperience();
                    hrmEx.hrm_employee_official_id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id;
                    hrmEx.company_name = Request.Form.GetValues("company_name[]")[i].ToString();
                    hrmEx.job_title = Request.Form.GetValues("job_title[]")[i].ToString();
                    hrmEx.dept_name = Request.Form.GetValues("dept_name[]")[i].ToString();
                    hrmEx.from_date = DateTime.Parse(Request.Form.GetValues("from_date[]")[i].ToString());
                    hrmEx.to_date = DateTime.Parse(Request.Form.GetValues("to_date[]")[i].ToString());
                    hrmEx.responsibility = Request.Form.GetValues("responsibility[]")[i].ToString();
                    vmdl.VM_HRM_EMPLOYEE_EXPERIENCES.Add(hrmEx);
                }

            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "Please Enter at least one Experience Info", ModelState);
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index");
            }
            //remove
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeExperiences.RemoveRange(contxt.HrmEmployeeExperiences.Where(x => x.hrm_employee_official_id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id));
                contxt.SaveChanges();
            }
            //add
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeExperiences.AddRange(vmdl.VM_HRM_EMPLOYEE_EXPERIENCES);
                contxt.SaveChanges();
            }
            GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
            TempData["ViewModel"] = vmdl;
            return RedirectToAction("index");

        }


        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.Create })]
        public JsonResult JsonEmployeeLoad(String empID)
        {
            int eID = Convert.ToInt32(empID);
            if (new DBContext().HrmEmployeeOfficials.Where(x => x.id == eID).Count() > 0)
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.Find(eID);
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture = Utility.GetBaseUrl() + "/" + vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture;
                vmdl.VM_HRM_DESIGNATION = new DBContext().HrmDesignations.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.designation_id);
                vmdl.VM_DEPARTMENT = new DBContext().Departments.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
                vmdl.VM_HRM_EMPLOYEE_EXPERIENCES = new DBContext().HrmEmployeeExperiences.Where(x => x.hrm_employee_official_id == eID).ToList();
                foreach(HrmEmployeeExperience exp in vmdl.VM_HRM_EMPLOYEE_EXPERIENCES)
                {
                    exp.from_date_json = exp.from_date.ToShortDateString();
                    exp.to_date_json = exp.to_date.ToShortDateString();
                    exp.duration_days = exp.to_date.Subtract(exp.from_date).Days;
                }
                vmdl.CommitStatus = true;
            }
            else
            {
                vmdl.CommitStatus = false;
            }
            return Json(vmdl, JsonRequestBehavior.AllowGet);
        }
    }
}