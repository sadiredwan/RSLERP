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
        /// for show all HrmEmployeePersonalInfo list
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

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmEmployeePersonalInfo
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

            int ID = Convert.ToInt32(id);

            //pass model to view
            HrmEmployeePersonalInfo mdlHrmEmployeePersonalInfo = new HrmEmployeePersonalInfo();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmEmployeePersonalInfo already exist
                if (new DBContext().HrmEmployeePersonalInfos.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == ID).Count > 0)
                {
                    //pass model to view with HrmEmployeePersonalInfo info
                    mdlHrmEmployeePersonalInfo = new DBContext().HrmEmployeePersonalInfos.Find(ID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_PERSONAL_INFO = mdlHrmEmployeePersonalInfo;
            }
            
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
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

        public PartialViewResult _PartialTable(String employee_official_id)
        {
            ViewModel vmdl = new ViewModel();
            vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new DBContext().HrmEmployeeOfficials.ToList().Find(x => x.id == Convert.ToInt32(employee_official_id));
            vmdl.VM_DEPARTMENT = new DBContext().Departments.ToList().Find(x => x.id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.department_id);
            vmdl.VM_HRM_SECTION = new DBContext().HrmSections.ToList().Find(x => x.id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.section_id);
            vmdl.VM_HRM_SUB_SECTION = new DBContext().HrmSubSections.ToList().Find(x => x.id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.subsection_id);
            return PartialView(vmdl);
        }

    }
}