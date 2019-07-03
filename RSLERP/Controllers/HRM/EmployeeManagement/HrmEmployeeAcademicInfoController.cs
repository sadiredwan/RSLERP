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
    public class HrmEmployeeAcademicInfoController : Controller
    {
        ViewModel vmdl = new ViewModel();
        // GET: HrmEmployeeAcademicInfo
        public ActionResult Index()
        {
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
          
            vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFO = new HrmEmployeeAcademicInfo();
            
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }
            vmdl.VM_HRM_EDUCATIONS_LEVELS = new DBContext().HrmEducationLevels.ToList();
            vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = new DBContext().HrmEmployeeAcademicInfos.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
            return View(vmdl);
        }


        /// <summary>
        /// Create Page for HrmEmployeeAcademic Info
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

            int eoID = Convert.ToInt32(id);
            
            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmEmployeeOfficial already exist
                if (new DBContext().HrmEmployeeAcademicInfos.Where(x => x.id == eoID).Count() > 0)
                {
                    //pass model to view with HrmEmployeeOfficial info
                    vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFO = new DBContext().HrmEmployeeAcademicInfos.Find(eoID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            //pass model to view
            vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFO = new HrmEmployeeAcademicInfo();
            vmdl.VM_HRM_EDUCATIONS_LEVELS = new DBContext().HrmEducationLevels.ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store  
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
            vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = new List<HrmEmployeeAcademicInfo>();           
                
            if (Request.Form.GetValues("education_level_id[]") != null)
            {
                int totalEdulvs = Request.Form.GetValues("education_level_id[]").Count();
                for(int i=0;i<totalEdulvs;i++)
                {
                    HrmEmployeeAcademicInfo hrmAc = new HrmEmployeeAcademicInfo();
                    hrmAc.HrmEmployeeOfficial_ID = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id;
                    hrmAc.education_level_id = Convert.ToInt32(Request.Form.GetValues("education_level_id[]")[i].ToString());
                    hrmAc.institute_name = Request.Form.GetValues("institute_name[]")[i].ToString();
                    hrmAc.major_group = Request.Form.GetValues("major_group[]")[i].ToString();
                    hrmAc.passingyear = Request.Form.GetValues("passingyear[]")[i].ToString();
                    hrmAc.result = Request.Form.GetValues("result[]")[i].ToString();
                    hrmAc.edu_board = Request.Form.GetValues("edu_board[]")[i].ToString();
                    vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS.Add(hrmAc);
                }                  
                    
            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false,"Please Enter at least one Academic Info", ModelState);
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index");
            }
            //remove
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeAcademicInfos.RemoveRange(contxt.HrmEmployeeAcademicInfos.Where(x => x.HrmEmployeeOfficial_ID == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id));
                contxt.SaveChanges();
            }
            //add
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeAcademicInfos.AddRange(vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS);
                contxt.SaveChanges();
            }
            GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);    
            TempData["ViewModel"] = vmdl;
            return RedirectToAction("index");                      
           

        }



        public JsonResult JsonEmployeeLoad(String empID)
        {
            int eID = Convert.ToInt32(empID);
            if (new DBContext().HrmEmployeeOfficials.Where(x => x.id == eID).Count() > 0)
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.Find(eID);
                vmdl.VM_HRM_DESIGNATION = new DBContext().HrmDesignations.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.designation_id);
                vmdl.VM_DEPARTMENT = new DBContext().Departments.Find(vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
                vmdl.VM_IQUERY = null;
                vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = new DBContext().HrmEmployeeAcademicInfos.Where(x => x.HrmEmployeeOfficial_ID == eID).ToList();
                using (var contxt = new DBContext())
                {
                    vmdl.VM_HRM_EMPLOYEE_ACADEMIC_INFOS = (from ac in contxt.HrmEmployeeAcademicInfos
                                                           join lvl in contxt.HrmEducationLevels
                                                           on ac.education_level_id equals lvl.id
                                                           where ac.HrmEmployeeOfficial_ID == eID
                                                           orderby ac.id
                                                           select new
                                                           {
                                                               id = ac.id,
                                                               HrmEmployeeOfficial_ID = ac.HrmEmployeeOfficial_ID,
                                                               education_level_id = ac.education_level_id,
                                                               EducationlevelName = lvl.level_name,
                                                               major_group = ac.major_group,
                                                               edu_board = ac.edu_board,
                                                               institute_name = ac.institute_name,
                                                               passingyear = ac.passingyear,
                                                               result = ac.result,

                                                           }).ToList().Select(x => new HrmEmployeeAcademicInfo()
                                                           {
                                                               id = x.id,
                                                               HrmEmployeeOfficial_ID = x.HrmEmployeeOfficial_ID,
                                                               education_level_id = x.education_level_id,
                                                               EducationlevelName = x.EducationlevelName,
                                                               major_group = x.major_group,
                                                               edu_board = x.edu_board,
                                                               institute_name = x.institute_name,
                                                               passingyear = x.passingyear,
                                                               result = x.result
                                                           }).ToList();


                    vmdl.CommitStatus = true;
                }
            }else
            {
                vmdl.CommitStatus = false;
            }
            return Json(vmdl, JsonRequestBehavior.AllowGet);
        }

      

    }
}