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
    public class SlsSDPController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all SlsSDP list
        /// </summary>
        /// <returns></returns>
        // GET: SlsSDP
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_SLSSDPS = new DBContext().SlsSDPs.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_SLSSDP_TYPES = new DBContext().SlsSDPTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_ANFCOAS = new DBContext().AnFCOAs.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for SlsSDP
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

            int sdpID = Convert.ToInt32(id);

            //pass model to view
            SlsSDP mdlSlsSDP = new SlsSDP();

            //Check if id doesnot null
            if (id != null)
            {
                //check if SlsSDP already exist
                if (new DBContext().SlsSDPs.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == sdpID).Count > 0)
                {
                    //pass model to view with SlsSDP info
                    mdlSlsSDP = new DBContext().SlsSDPs.Find(sdpID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_SLSSDP = mdlSlsSDP;
            }

            vmdl.VM_SLSSDPS = new DBContext().SlsSDPs.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_SLSSDP_TYPES = new DBContext().SlsSDPTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_ANFCOAS = new DBContext().AnFCOAs.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store SlsSDP 
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
                if (new DBContext().SlsSDPs.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_SLSSDP.id).Count > 0)
                {
                    //Update SlsSDP
                    //VM_SLSSDP.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.SlsSDPs.Attach(vmdl.VM_SLSSDP);
                        contxt.Entry(vmdl.VM_SLSSDP).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new SlsSDP
                    using (var contxt = new DBContext())
                    {
                        contxt.SlsSDPs.Add(vmdl.VM_SLSSDP);
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