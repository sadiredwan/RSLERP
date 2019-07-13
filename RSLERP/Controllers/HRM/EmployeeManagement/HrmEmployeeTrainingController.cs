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
    public class HrmEmployeeTrainingController : Controller
    {
        ViewModel vmdl = new ViewModel();

        // GET: HrmEmployeeTraining
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.View })]
        public ActionResult Index()
        {
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            vmdl.VM_HRM_EMPLOYEE_TRAINING = new HrmEmployeeTraining();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }

            vmdl.VM_HRM_EMPLOYEE_TRAININGS = new DBContext().HrmEmployeeTrainings.Where(x => x.CompanyId == COMPANY_ID).ToList();
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
            vmdl.VM_HRM_EMPLOYEE_TRAININGS = new List<HrmEmployeeTraining>();

            if (Request.Form.GetValues("training_title[]") != null)
            {
                int totalTrainings = Request.Form.GetValues("training_title[]").Count();
                for (int i = 0; i < totalTrainings; i++)
                {
                    HrmEmployeeTraining hrmTr = new HrmEmployeeTraining();
                    hrmTr.hrm_employee_official_id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id;
                    hrmTr.training_title = Request.Form.GetValues("training_title[]")[i].ToString();
                    hrmTr.venue = Request.Form.GetValues("venue[]")[i].ToString();
                    hrmTr.dept_name = Request.Form.GetValues("dept_name[]")[i].ToString();
                    hrmTr.from_date = DateTime.Parse(Request.Form.GetValues("from_date[]")[i].ToString());
                    hrmTr.to_date = DateTime.Parse(Request.Form.GetValues("to_date[]")[i].ToString());
                    hrmTr.subject = Request.Form.GetValues("subject[]")[i].ToString();
                    hrmTr.duration_days = Convert.ToInt32(Request.Form.GetValues("duration_days[]")[i]);
                    vmdl.VM_HRM_EMPLOYEE_TRAININGS.Add(hrmTr);
                }

            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "Please Enter at least one Training Info", ModelState);
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index");
            }
            //remove
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeTrainings.RemoveRange(contxt.HrmEmployeeTrainings.Where(x => x.hrm_employee_official_id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id));
                contxt.SaveChanges();
            }
            //add
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeTrainings.AddRange(vmdl.VM_HRM_EMPLOYEE_TRAININGS);
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
                vmdl.VM_HRM_EMPLOYEE_TRAININGS = new DBContext().HrmEmployeeTrainings.Where(x => x.hrm_employee_official_id == eID).ToList();
                foreach (HrmEmployeeTraining trn in vmdl.VM_HRM_EMPLOYEE_TRAININGS)
                {
                    trn.from_date_json = trn.from_date.ToShortDateString();
                    trn.to_date_json = trn.to_date.ToShortDateString();
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