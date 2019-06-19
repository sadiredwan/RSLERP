using RSLERP.DataManager;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RSLERP.DataManager.Utility;


namespace RSLERP.Controllers.Settings
{
    public class UsersController : Controller
    {
        ViewModel vmdl = new ViewModel();
        Utility util = new Utility();
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
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.ToList();


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
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";


            int dID = Convert.ToInt32(id);


            //pass model to view
            CompanyUser mdlCompany = new CompanyUser();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Company already exist
                if (new DBContext().CompanyUsers.ToList().FindAll(x => x.u_ID == dID).Count > 0)
                {
                    //pass model to view with Company info
                    mdlCompany = new DBContext().CompanyUsers.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_COMPANY_USER = mdlCompany;
            }




            return View(vmdl);
        }

        /// <summary>
        /// Store Company 
        /// Create or Update
        /// </summary>
        /// <param name="CompanyMdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Get Current UserName



            //Check Model state is valid or not
            if (ModelState.IsValid)
            {
                
                //check if already exist then update
                if (new DBContext().CompanyUsers.ToList().FindAll(x => x.u_ID == vmdl.VM_COMPANY_USER.u_ID).Count > 0)
                {
                    CompanyUser findUser = new DBContext().CompanyUsers.Find(vmdl.VM_COMPANY_USER.u_ID);
                    vmdl.VM_COMPANY_USER.u_ParentUserID = findUser.u_ParentUserID;
                    vmdl.VM_COMPANY_USER.u_Type = 0;
                    //Update Company
                    //CompanyMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.CompanyUsers.Attach(vmdl.VM_COMPANY_USER);
                        contxt.Entry(vmdl.VM_COMPANY_USER).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    vmdl.VM_COMPANY_USER.u_Password = vmdl.VM_COMPANY_USER.confirm_password = util.Encode(vmdl.VM_COMPANY_USER.u_Password);
                    //Add new Company
                    using (var contxt = new DBContext())
                    {
                        vmdl.VM_COMPANY_USER.u_Type = 0;
                        vmdl.VM_COMPANY_USER.u_ParentUserID = RSLERPApplication.CurrentState().user_id;
                        contxt.CompanyUsers.Add(vmdl.VM_COMPANY_USER);
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

        public ActionResult LoadPassowrdChange(String id)
        {
            //Get Current UserName
            // int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";


            int dID = Convert.ToInt32(id);


            //pass model to view
            CompanyUser mdlCompany = new CompanyUser();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Company already exist
                if (new DBContext().CompanyUsers.ToList().FindAll(x => x.u_ID == dID).Count > 0)
                {
                    //pass model to view with Company info
                    mdlCompany = new DBContext().CompanyUsers.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_COMPANY_USER = mdlCompany;
            }




            return View(vmdl);
        }

        [HttpPost]
        public ActionResult StorePassowrdChange(ViewModel vmdl)
        {
            //Get Current UserName
            
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().CompanyUsers.ToList().FindAll(x => x.u_ID == vmdl.VM_COMPANY_USER.u_ID).Count > 0)
                {
                    CompanyUser findUser = new DBContext().CompanyUsers.Find(vmdl.VM_COMPANY_USER.u_ID);
                    findUser.u_Password = vmdl.VM_COMPANY_USER.u_Password;

                    vmdl.VM_COMPANY_USER = findUser;
                    vmdl.VM_COMPANY_USER.u_Password = vmdl.VM_COMPANY_USER.confirm_password = util.Encode(vmdl.VM_COMPANY_USER.u_Password);
                    try
                    {
                        //Update Company
                        //CompanyMdl.updated_at = DateTime.Now;
                        using (var contxt = new DBContext())
                        {
                            contxt.CompanyUsers.Attach(vmdl.VM_COMPANY_USER);
                            contxt.Entry(vmdl.VM_COMPANY_USER).State = EntityState.Modified;
                            contxt.SaveChanges();

                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    vmdl.VM_COMPANY_USER.u_Password = vmdl.VM_COMPANY_USER.confirm_password = util.Encode(vmdl.VM_COMPANY_USER.u_Password);
                    //Add new Company
                    using (var contxt = new DBContext())
                    {
                        vmdl.VM_COMPANY_USER.u_Type = 0;
                        vmdl.VM_COMPANY_USER.u_ParentUserID = RSLERPApplication.CurrentState().user_id;
                        contxt.CompanyUsers.Add(vmdl.VM_COMPANY_USER);
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
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false,errors, ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("LoadPassowrdChange");
            }
        }






    }
}