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
    public class HrmEmployeePublisherController : Controller
    {
        ViewModel vmdl = new ViewModel();

        // GET: HrmEmployeePublisher
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] { AccessLevel.View })]
        public ActionResult Index()
        {
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            vmdl.VM_HRM_EMPLOYEE_PUBLISHER = new HrmEmployeePublisher();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = new HrmEmployeeOfficial();
            }

            vmdl.VM_HRM_EMPLOYEE_PUBLISHERS = new DBContext().HrmEmployeePublishers.Where(x => x.CompanyId == COMPANY_ID).ToList();
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
            vmdl.VM_HRM_EMPLOYEE_PUBLISHERS = new List<HrmEmployeePublisher>();

            if (Request.Form.GetValues("title[]") != null)
            {
                int totalPublishers = Request.Form.GetValues("title[]").Count();
                for (int i = 0; i < totalPublishers; i++)
                {
                    HrmEmployeePublisher hrmPb = new HrmEmployeePublisher();
                    hrmPb.hrm_employee_official_id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id;
                    hrmPb.title = Request.Form.GetValues("title[]")[i].ToString();
                    hrmPb.publisher = Request.Form.GetValues("publisher[]")[i].ToString();
                    hrmPb.publishing_date = DateTime.Parse(Request.Form.GetValues("publishing_date[]")[i].ToString());
                    hrmPb.ref_no = Request.Form.GetValues("ref_no[]")[i].ToString();
                    hrmPb.vol_no = Request.Form.GetValues("vol_no[]")[i].ToString();
                    hrmPb.author = Request.Form.GetValues("author[]")[i].ToString();
                    vmdl.VM_HRM_EMPLOYEE_PUBLISHERS.Add(hrmPb);
                }

            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "Please Enter at least one Publication Info", ModelState);
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index");
            }
            //remove
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeePublishers.RemoveRange(contxt.HrmEmployeePublishers.Where(x => x.hrm_employee_official_id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id));
                contxt.SaveChanges();
            }
            //add
            using (var contxt = new DBContext())
            {
                contxt.HrmEmployeePublishers.AddRange(vmdl.VM_HRM_EMPLOYEE_PUBLISHERS);
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
                vmdl.VM_HRM_EMPLOYEE_PUBLISHERS = new DBContext().HrmEmployeePublishers.Where(x => x.hrm_employee_official_id == eID).ToList();
                foreach (HrmEmployeePublisher epb in vmdl.VM_HRM_EMPLOYEE_PUBLISHERS)
                {
                    epb.publishing_date_json = epb.publishing_date.ToShortDateString();
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