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
    public class InvWarehouseTypeController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvWarehouseType list
        /// </summary>
        /// <returns></returns>
        // GET: InvWarehouseType
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_INVWAREHOUSE_TYPES = new DBContext().InvWarehouseTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvWarehouseType 
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

            int wID = Convert.ToInt32(id);

            //pass model to view
            InvWarehouseType mdlInvWarehouseType = new InvWarehouseType();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvWarehouseType already exist
                if (new DBContext().InvWarehouseTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == wID).Count > 0)
                {
                    //pass model to view with InvWarehouseType info
                    mdlInvWarehouseType = new DBContext().InvWarehouseTypes.Find(wID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVWAREHOUSE_TYPE = mdlInvWarehouseType;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store InvWarehouseType 
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
                if (new DBContext().InvWarehouseTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVWAREHOUSE_TYPE.id).Count > 0)
                {
                    //Update InvWarehouseType
                    //VM_INVWAREHOUSE_TYPE.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvWarehouseTypes.Attach(vmdl.VM_INVWAREHOUSE_TYPE);
                        contxt.Entry(vmdl.VM_INVWAREHOUSE_TYPE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvWarehouseType
                    using (var contxt = new DBContext())
                    {
                        contxt.InvWarehouseTypes.Add(vmdl.VM_INVWAREHOUSE_TYPE);
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