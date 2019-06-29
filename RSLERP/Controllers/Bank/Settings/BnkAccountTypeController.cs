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
    public class BnkAccountTypeController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all BnkAccountType list
        /// </summary>
        /// <returns></returns>
        // GET: BnkAccountType
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            
            vmdl.VM_BNK_ACCOUNT_TYPES = new DBContext().BnkAccountTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for BnkAccountType
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

            int batID = Convert.ToInt32(id);

            //pass model to view
            BnkAccountType mdlBnkAccountType = new BnkAccountType();

            //Check if id doesnot null
            if (id != null)
            {
                //check if BnkAccountType already exist
                if (new DBContext().BnkAccountTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == batID).Count > 0)
                {
                    //pass model to view with BnkAccountType info
                    mdlBnkAccountType = new DBContext().BnkAccountTypes.Find(batID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_BNK_ACCOUNT_TYPE = mdlBnkAccountType;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store BnkBranch 
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
                if (new DBContext().BnkAccountTypes.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_BNK_ACCOUNT_TYPE.id).Count > 0)
                {
                    //Update BnkAccountType
                    //VM_BNK_ACCOUNT_TYPE.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.BnkAccountTypes.Attach(vmdl.VM_BNK_ACCOUNT_TYPE);
                        contxt.Entry(vmdl.VM_BNK_ACCOUNT_TYPE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new BnkAccountType
                    using (var contxt = new DBContext())
                    {
                        contxt.BnkAccountTypes.Add(vmdl.VM_BNK_ACCOUNT_TYPE);
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