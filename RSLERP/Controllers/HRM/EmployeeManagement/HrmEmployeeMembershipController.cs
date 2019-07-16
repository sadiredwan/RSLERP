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
    public class HrmEmployeeMembershipController : Controller
    {
        ViewModel vmdl = new ViewModel();

        // GET: HrmEmployeeMembership
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.View })]
        public ActionResult Index()
        {
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            vmdl.VM_HRM_EMPLOYEE_MEMBERSHIP = new HrmEmployeeMembership();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }

            vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS = new DBContext().HrmEmployeeMemberships.Where(x => x.CompanyId == COMPANY_ID).ToList();
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
            vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS = new List<HrmEmployeeMembership>();

            if (Request.Form.GetValues("organization_name[]") != null)
            {
                int totalMemberships = Request.Form.GetValues("organization_name[]").Count();
                for (int i = 0; i < totalMemberships; i++)
                {
                    HrmEmployeeMembership hrmMs = new HrmEmployeeMembership();
                    hrmMs.hrm_employee_official_id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id;
                    hrmMs.organization_name = Request.Form.GetValues("organization_name[]")[i].ToString();
                    hrmMs.membership_category = Request.Form.GetValues("membership_category[]")[i].ToString();
                    hrmMs.membership_no = Request.Form.GetValues("membership_no[]")[i].ToString();
                    hrmMs.membership_date = DateTime.Parse(Request.Form.GetValues("membership_date[]")[i].ToString());
                    vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS.Add(hrmMs);
                }

            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "Please Enter at least one Membership Info", ModelState);
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index");
            }
            //remove
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeMemberships.RemoveRange(contxt.HrmEmployeeMemberships.Where(x => x.hrm_employee_official_id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id));
                contxt.SaveChanges();
            }
            //add
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeeMemberships.AddRange(vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS);
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
                vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS = new DBContext().HrmEmployeeMemberships.Where(x => x.hrm_employee_official_id == eID).ToList();
                foreach (HrmEmployeeMembership ems in vmdl.VM_HRM_EMPLOYEE_MEMBERSHIPS)
                {
                    ems.membership_date_json = ems.membership_date.ToShortDateString();
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