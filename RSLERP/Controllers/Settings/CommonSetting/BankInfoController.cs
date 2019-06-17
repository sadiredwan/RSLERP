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
    public class BankInfoController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all BankInfo list
        /// </summary>
        /// <returns></returns>
        // GET: BankInfo
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            vmdl.VM_BANK_INFOS = new DBContext().BankInfos.ToList();
            vmdl.VM_BANK_INFO_TYPES = new DBContext().BankInfoTypes.ToList();
            vmdl.VM_COUNTRIES = new DBContext().Countries.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for BankInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            //int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";

            int bID = Convert.ToInt32(id);

            //pass model to view
            BankInfo mdlBankInfo = new BankInfo();

            //Check if id doesnot null
            if (id != null)
            {
                //check if BankInfo already exist
                if (new DBContext().ProjectSegments.ToList().FindAll(x => x.id == bID).Count > 0)
                {
                    //pass model to view with BankInfo
                    mdlBankInfo = new DBContext().BankInfos.Find(bID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_BANK_INFO = mdlBankInfo;
            }
            vmdl.VM_BANK_INFO_TYPES = new DBContext().BankInfoTypes.ToList();
            vmdl.VM_COUNTRIES = new DBContext().Countries.ToList();
            return View(vmdl);
        }

        /// <summary>
        /// Store BankInfo 
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().BankInfos.ToList().FindAll(x => x.id == vmdl.VM_BANK_INFO.id).Count > 0)
                {
                    //Update BankInfo
                    //VM_BANK_INFO.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.BankInfos.Attach(vmdl.VM_BANK_INFO);
                        contxt.Entry(vmdl.VM_BANK_INFO).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new BankInfo
                    using (var contxt = new DBContext())
                    {

                        contxt.BankInfos.Add(vmdl.VM_BANK_INFO);
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
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errors, ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("load");
            }

        }



    }
}