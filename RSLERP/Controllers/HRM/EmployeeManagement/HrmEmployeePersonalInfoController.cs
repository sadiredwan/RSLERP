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
    public class HrmEmployeePersonalInfoController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        // GET: HrmEmployeePersonalInfo
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO = new HrmEmployeePersonalInfo();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.Find(vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO.hrm_employee_official_id);
                vmdl.VM_HRM_SECTION = new DBContext().HrmSections.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.section_id);
                vmdl.VM_HRM_SUB_SECTION = new DBContext().HrmSubSections.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.subsection_id);
                vmdl.VM_HRM_DESIGNATION = new DBContext().HrmDesignations.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.designation_id);
                vmdl.VM_DEPARTMENT = new DBContext().Departments.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }

            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_RELATIONS = new DBContext().HrmEmployeeRelations.ToList();
            return View(vmdl);
        }

        /// <summary>
        /// Store HrmEmployeePersonalInfo 
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
                if (new DBContext().HrmEmployeePersonalInfos.Where(x => x.CompanyId == COMPANY_ID).Where(x => x.hrm_employee_official_id == vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO.hrm_employee_official_id).ToList().Count > 0)
                {
                    //Update HrmEmployeePersonalInfo
                    //VM_HRM_SECTION.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmEmployeePersonalInfos.Attach(vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO);
                        contxt.Entry(vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmEmployeePersonalInfo
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmEmployeePersonalInfos.Add(vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO);
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

        public JsonResult JsonEmployeeLoad(String hrm_employee_official_id)
        {
            int eID = Convert.ToInt32(hrm_employee_official_id);
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            if (new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).Where(x => x.id == eID).Count() > 0)
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.Find(eID);
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture = Utility.GetBaseUrl() + "/" + vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture;
                vmdl.VM_HRM_SECTION = new DBContext().HrmSections.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.section_id);
                vmdl.VM_HRM_SUB_SECTION = new DBContext().HrmSubSections.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.subsection_id);
                vmdl.VM_HRM_DESIGNATION = new DBContext().HrmDesignations.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.designation_id);
                vmdl.VM_DEPARTMENT = new DBContext().Departments.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
                vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO = new DBContext().HrmEmployeePersonalInfos.Where(x => x.hrm_employee_official_id == eID).FirstOrDefault();
                vmdl.VM_IQUERY = null;
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