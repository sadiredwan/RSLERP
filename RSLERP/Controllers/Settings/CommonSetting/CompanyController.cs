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
    public class CompanyController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Companys list
        /// </summary>
        /// <returns></returns>
        // GET: Company
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
               
            }
                vmdl.VM_COMPANIES = new DBContext().Companies.ToList();
            
            
            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Company 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
           // int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Company
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";

            int dID = Convert.ToInt32(id);

            //pass model to view
            Company mdlCompany = new Company();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Company already exist
                if (new DBContext().Companies.ToList().FindAll(x => x.c_ID == dID).Count > 0)
                {
                    //pass model to view with Company info
                    mdlCompany = new DBContext().Companies.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] !=null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_COMPANE = mdlCompany;
            }
            
            return View(vmdl);
        }

        /// <summary>
        /// Store Company 
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
                if (new DBContext().Companies.ToList().FindAll(x => x.c_ID == vmdl.VM_COMPANE.c_ID).Count > 0)
                {
                    //Update Company
                    //CompanyMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Companies.Attach(vmdl.VM_COMPANE);
                        contxt.Entry(vmdl.VM_COMPANE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Company
                    using (var contxt = new DBContext())
                    {
                        contxt.Companies.Add(vmdl.VM_COMPANE);
                        contxt.SaveChanges();
                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index",vmdl);
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