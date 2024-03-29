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
    public class InvSupplierController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvSupplier list
        /// </summary>
        /// <returns></returns>
        // GET: InvSupplier
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_INVSUPPLIERS = new DBContext().InvSuppliers.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_ANFCOAS = new DBContext().AnFCOAs.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvSupplier 
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
            InvSupplier mdlInvSupplier = new InvSupplier();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvSupplier already exist
                if (new DBContext().InvSuppliers.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == sID).Count > 0)
                {
                    //pass model to view with InvSupplier info
                    mdlInvSupplier = new DBContext().InvSuppliers.Find(sID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVSUPPLIER = mdlInvSupplier;
            }
            vmdl.VM_INVSUPPLIERS = new DBContext().InvSuppliers.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_ANFCOAS = new DBContext().AnFCOAs.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_INVSUPPLIER_TYPES = new DBContext().InvSupplierTypes.ToList();
            vmdl.VM_COUNTRIES = new DBContext().Countries.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Store InvSupplier 
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
                if (new DBContext().InvSuppliers.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVSUPPLIER.id).Count > 0)
                {
                    //Update InvSupplier
                    //VM_INVSUPPLIER.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvSuppliers.Attach(vmdl.VM_INVSUPPLIER);
                        contxt.Entry(vmdl.VM_INVSUPPLIER).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvSupplier
                    using (var contxt = new DBContext())
                    {
                        contxt.InvSuppliers.Add(vmdl.VM_INVSUPPLIER);
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