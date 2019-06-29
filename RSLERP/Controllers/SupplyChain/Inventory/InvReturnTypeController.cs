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
    public class InvReturnTypeController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all InvReturnType list
        /// </summary>
        /// <returns></returns>
        // GET: InvReturnType
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_INVRETURN_TYPES = new DBContext().InvReturnTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for InvReturnType
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

            int rtID = Convert.ToInt32(id);

            //pass model to view
            InvReturnType mdlInvReturnType = new InvReturnType();

            //Check if id doesnot null
            if (id != null)
            {
                //check if InvReturnType already exist
                if (new DBContext().InvReturnTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == rtID).Count > 0)
                {
                    //pass model to view with InvReturnType info
                    mdlInvReturnType = new DBContext().InvReturnTypes.Find(rtID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_INVRETURN_TYPE = mdlInvReturnType;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store InvReturnType 
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
                if (new DBContext().InvReturnTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_INVRETURN_TYPE.id).Count > 0)
                {
                    //Update InvReturnType
                    //VM_INVRETURN_TYPE.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.InvReturnTypes.Attach(vmdl.VM_INVRETURN_TYPE);
                        contxt.Entry(vmdl.VM_INVRETURN_TYPE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new InvReturnType
                    using (var contxt = new DBContext())
                    {
                        contxt.InvReturnTypes.Add(vmdl.VM_INVRETURN_TYPE);
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