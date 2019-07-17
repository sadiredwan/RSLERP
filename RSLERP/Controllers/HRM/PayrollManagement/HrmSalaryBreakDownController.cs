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
    public class HrmSalaryBreakDownController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        // GET: HrmSalaryBreakDown
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            vmdl.VM_HRM_SALARY_BREAKDOWN = new HrmSalaryBreakDown();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
                vmdl.VM_HRM_EMPLOYEE_TYPE = new DBContext().HrmEmployeeTypes.Find(vmdl.VM_HRM_SALARY_BREAKDOWN.hrm_employee_type_id);
                vmdl.VM_HRM_SALARY_BREAKDOWN = new DBContext().HrmSalaryBreakDowns.Find(vmdl.VM_HRM_SALARY_BREAKDOWN.id);
            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_TYPE = new HrmEmployeeType();
            }

            vmdl.VM_HRM_EMPLOYEE_TYPES = new DBContext().HrmEmployeeTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmSalaryBreakDown 
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
                if (new DBContext().HrmSalaryBreakDowns.Where(x => x.CompanyId == COMPANY_ID).Where(x => x.hrm_employee_type_id == vmdl.VM_HRM_SALARY_BREAKDOWN.hrm_employee_type_id).ToList().Count > 0)
                {
                    //Update HrmSalaryBreakDown
                    //VM_HRM_SALARY_BREAKDOWN.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmSalaryBreakDowns.Attach(vmdl.VM_HRM_SALARY_BREAKDOWN);
                        contxt.Entry(vmdl.VM_HRM_SALARY_BREAKDOWN).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmSalaryBreakDown
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmSalaryBreakDowns.Add(vmdl.VM_HRM_SALARY_BREAKDOWN);
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
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errorstate.errors(ModelState), ModelState);
                //GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("index");
            }

        }

        public JsonResult JsonEmployeeLoad(String hrm_employee_type_id)
        {
            int eID = Convert.ToInt32(hrm_employee_type_id);
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            if (new DBContext().HrmEmployeeTypes.Where(x => x.id == eID).Count() > 0)
            {
                vmdl.VM_HRM_EMPLOYEE_TYPE = new DBContext().HrmEmployeeTypes.Find(eID);
                vmdl.VM_HRM_SALARY_BREAKDOWN = new DBContext().HrmSalaryBreakDowns.Where(x => x.hrm_employee_type_id == eID).FirstOrDefault();
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