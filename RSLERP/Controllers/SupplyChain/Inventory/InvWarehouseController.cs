﻿using RSLERP.DataManager;
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
    public class InvWarehouseController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvWarehouse list
        /// </summary>
        /// <returns></returns>
        // GET: InvWarehouse
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_INVWAREHOUSES = new DBContext().InvWarehouses.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_INVWAREHOUSE_TYPES = new DBContext().InvWarehouseTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvWarehouse 
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
            InvWarehouse mdlInvWarehouse = new InvWarehouse();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvWarehouse already exist
                if (new DBContext().InvWarehouses.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == wID).Count > 0)
                {
                    //pass model to view with InvWarehouse info
                    mdlInvWarehouse = new DBContext().InvWarehouses.Find(wID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVWAREHOUSE = mdlInvWarehouse;
            }

            vmdl.VM_INVWAREHOUSE_TYPES = new DBContext().InvWarehouseTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store InvWarehouse 
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
                if (new DBContext().InvWarehouses.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVWAREHOUSE.id).Count > 0)
                {
                    //Update InvWarehouse
                    //VM_INVWAREHOUSE.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvWarehouses.Attach(vmdl.VM_INVWAREHOUSE);
                        contxt.Entry(vmdl.VM_INVWAREHOUSE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvWarehouse
                    using (var contxt = new DBContext())
                    {
                        contxt.InvWarehouses.Add(vmdl.VM_INVWAREHOUSE);
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