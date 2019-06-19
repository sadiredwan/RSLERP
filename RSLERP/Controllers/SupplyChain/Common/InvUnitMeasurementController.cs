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
    public class InvUnitMeasurementController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvUnitMeasurement list
        /// </summary>
        /// <returns></returns>
        // GET: InvUnitMeasurement
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_INVUNIT_MEASUREMENTS = new DBContext().InvUnitMeasurements.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvUnitMeasurement 
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

            int umID = Convert.ToInt32(id);

            //pass model to view
            InvUnitMeasurement mdlInvUnitMeasurement = new InvUnitMeasurement();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvUnitMeasurement already exist
                if (new DBContext().InvUnitMeasurements.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == umID).Count > 0)
                {
                    //pass model to view with InvUnitMeasurement info
                    mdlInvUnitMeasurement = new DBContext().InvUnitMeasurements.Find(umID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVUNIT_MEASUREMENT = mdlInvUnitMeasurement;
            }
            vmdl.VM_INVUNIT_MEASUREMENTS = new DBContext().InvUnitMeasurements.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store InvUnitMeasurement 
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
                if (new DBContext().InvUnitMeasurements.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVUNIT_MEASUREMENT.id).Count > 0)
                {
                    //Update InvUnitMeasurement
                    //VM_INVUNIT_MEASUREMENT.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvUnitMeasurements.Attach(vmdl.VM_INVUNIT_MEASUREMENT);
                        contxt.Entry(vmdl.VM_INVUNIT_MEASUREMENT).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvUnitMeasurement
                    using (var contxt = new DBContext())
                    {
                        contxt.InvUnitMeasurements.Add(vmdl.VM_INVUNIT_MEASUREMENT);
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