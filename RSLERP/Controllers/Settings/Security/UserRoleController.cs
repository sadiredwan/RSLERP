using RSLERP.Authorization;
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
namespace RSLERP.Controllers.Settings.Security
{
    public class UserRoleController : Controller
    {
        ViewModel vmdl = new ViewModel();
        Utility util = new Utility();
        /// <summary>
        /// Index page 
        /// for show all Companys list
        /// </summary>
        /// <returns></returns>
        // GET: Company
        [SecurityAuthAuthorize(AccessLevels = new AccessLevel[] {AccessLevel.View})]
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
            UserRole mdl = new UserRole();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Company already exist
                if (new DBContext().UserRoles.ToList().FindAll(x => x.ur_u_ID == dID).Count > 0)
                {
                    //pass model to view with Company info
                    mdl = new DBContext().UserRoles.Where(x=>x.ur_u_ID==dID).FirstOrDefault();
                    vmdl.VM_ROLE = new DBContext().Roles.Find(mdl.ur_rl_ID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
                vmdl.VM_ROLE = new Role();
            }
            else
            {
                vmdl.VM_USER_ROLE = mdl;
            }

            ApplicationState app = RSLERPApplication.CurrentState();

            vmdl.VM_COMPANY_USER = new DBContext().CompanyUsers.Find(dID);
            vmdl.VM_ROLES = new DBContext().Roles.Where(x => x.CompanyId == RSLERPApplication.CurrentState().company_id).ToList();
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

                //check if already exist then delete
                if (new DBContext().UserRoles.Where(x => x.ur_u_ID == vmdl.VM_USER_ROLE.ur_u_ID).Count() > 0)
                {
                    UserRole findUserROle = new DBContext().UserRoles.Where(x => x.ur_u_ID == vmdl.VM_USER_ROLE.ur_u_ID).FirstOrDefault();
                    //Delete The Designation
                    using (var contxt = new DBContext())
                    {
                        contxt.Entry(findUserROle).State = EntityState.Deleted;
                        contxt.SaveChanges();
                    }
                }
                   
                  
                    //Add new Company
                    using (var contxt = new DBContext())
                    {
                        contxt.UserRoles.Add(vmdl.VM_USER_ROLE);
                        contxt.SaveChanges();

                    }

                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                
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