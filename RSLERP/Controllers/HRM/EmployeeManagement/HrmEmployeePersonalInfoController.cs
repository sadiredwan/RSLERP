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

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFOS = new DBContext().HrmEmployeePersonalInfos.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_RELATIONS = new DBContext().HrmEmployeeRelations.ToList();
            vmdl.VM_DEPARTMENTS = new DBContext().Departments.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SECTIONS = new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SUB_SECTIONS = new DBContext().HrmSubSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
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
                if (new DBContext().HrmEmployeePersonalInfos.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO.id).Count > 0)
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
                //GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errorstate.errors(ModelState), ModelState);
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("load");
            }

        }

        public JsonResult JsonEmployeeLoad(String empID)
        {
            int eID = Convert.ToInt32(empID);
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            if (new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).Where(x => x.id == eID).Count() > 0)
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.Find(eID);
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture = Utility.GetBaseUrl() + "/" + vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture;
                vmdl.VM_HRM_DESIGNATION = new DBContext().HrmDesignations.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.designation_id);
                vmdl.VM_DEPARTMENT = new DBContext().Departments.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
                //vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = new DBContext().HrmEmployeeAcademicInfos.Where(x => x.HrmEmployeeOfficial_ID == eID).ToList();
                //using (var contxt = new DBContext())
                //{
                //    vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = (from ac in contxt.HrmEmployeeAcademicInfos
                //                                           join lvl in contxt.HrmEducationLevels
                //                                           on ac.education_level_id equals lvl.id
                //                                           where ac.HrmEmployeeOfficial_ID == eID
                //                                           orderby ac.id
                //                                           select new
                //                                           {
                //                                               id = ac.id,
                //                                               HrmEmployeeOfficial_ID = ac.HrmEmployeeOfficial_ID,
                //                                               education_level_id = ac.education_level_id,
                //                                               EducationlevelName = lvl.level_name,
                //                                               major_group = ac.major_group,
                //                                               edu_board = ac.edu_board,
                //                                               institute_name = ac.institute_name,
                //                                               passingyear = ac.passingyear,
                //                                               result = ac.result,

                //                                           }).ToList().Select(x => new HrmEmployeeAcademicInfo()
                //                                           {
                //                                               id = x.id,
                //                                               HrmEmployeeOfficial_ID = x.HrmEmployeeOfficial_ID,
                //                                               education_level_id = x.education_level_id,
                //                                               EducationlevelName = x.EducationlevelName,
                //                                               major_group = x.major_group,
                //                                               edu_board = x.edu_board,
                //                                               institute_name = x.institute_name,
                //                                               passingyear = x.passingyear,
                //                                               result = x.result
                //                                           }).ToList();


                //    vmdl.CommitStatus = true;
                //}
            }
            else
            {
                vmdl.CommitStatus = false;
            }
            return Json(vmdl, JsonRequestBehavior.AllowGet);
        }

    }
}