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
    public class HrmShiftController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmShift list
        /// </summary>
        /// <returns></returns>
        // GET: HrmShift
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_SHIFTS = new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmShift
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

            int sID = Convert.ToInt32(id);

            //pass model to view
            HrmShift mdlHrmShift = new HrmShift();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmShift already exist
                if (new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == sID).Count > 0)
                {
                    //pass model to view with HrmShift info
                    mdlHrmShift = new DBContext().HrmShifts.Find(sID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_SHIFT = mdlHrmShift;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmShift 
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

                vmdl.VM_HRM_SHIFT.from_time = new TimeSpan(vmdl.VM_HRM_SHIFT.from_hour, vmdl.VM_HRM_SHIFT.from_minute, 0);
                vmdl.VM_HRM_SHIFT.to_time = new TimeSpan(vmdl.VM_HRM_SHIFT.to_hour, vmdl.VM_HRM_SHIFT.to_minute, 0);
                vmdl.VM_HRM_SHIFT.working_hours = vmdl.VM_HRM_SHIFT.to_time - vmdl.VM_HRM_SHIFT.from_time;

                //check if already exist then update
                if (new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_SHIFT.id).Count > 0)
                {
                    //Update HrmShift
                    //VM_HRM_SHIFT.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmShifts.Attach(vmdl.VM_HRM_SHIFT);
                        contxt.Entry(vmdl.VM_HRM_SHIFT).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmShift
                    using (var contxt = new DBContext())
                    {
                        contxt.HrmShifts.Add(vmdl.VM_HRM_SHIFT);
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