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
    public class HrmAttendenceTimeSetupController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmAttendenceTimeSetup list
        /// </summary>
        /// <returns></returns>
        // GET: HrmAttendenceTimeSetup
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_ATTENDENCE_TIME_SETUPS = new DBContext().HrmAttendenceTimeSetups.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SHIFTS = new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmAttendenceTimeSetup
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
            HrmAttendenceTimeSetup mdlHrmAttendenceTimeSetup = new HrmAttendenceTimeSetup();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmAttendenceTimeSetup already exist
                if (new DBContext().HrmAttendenceTimeSetups.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == ID).Count > 0)
                {
                    //pass model to view with HrmAttendenceTimeSetup info
                    mdlHrmAttendenceTimeSetup = new DBContext().HrmAttendenceTimeSetups.Find(ID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_ATTENDENCE_TIME_SETUP = mdlHrmAttendenceTimeSetup;
            }

            vmdl.VM_HRM_SHIFTS = new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmAttendenceTimeSetup 
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

                vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.late_time = new TimeSpan(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.late_hour, vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.late_minute, 0);
                vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.absent_time = new TimeSpan(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.absent_hour, vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.absent_minute, 0);
                vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.half_day_leave_time = new TimeSpan(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.half_day_leave_hour, vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.half_day_leave_minute, 0);

                //check if already exist then update
                if (new DBContext().HrmAttendenceTimeSetups.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_ATTENDENCE_TIME_SETUP.id).Count > 0)
                {
                    //Update HrmAttendenceTimeSetup
                    //VM_HRM_ATTENDENCE_TIME_SETUP.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmAttendenceTimeSetups.Attach(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP);
                        contxt.Entry(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmAttendenceTimeSetup
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmAttendenceTimeSetups.Add(vmdl.VM_HRM_ATTENDENCE_TIME_SETUP);
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