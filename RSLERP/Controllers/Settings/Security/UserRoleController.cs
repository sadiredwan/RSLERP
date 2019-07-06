using Newtonsoft.Json;
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
using System.Web.Script.Serialization;
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
            //vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.ToList();


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
           // vmdl.VM_ROLES = new DBContext().Roles.Where(x => x.CompanyId == app.company_id).ToList();
            vmdl.VM_ROLES = new DBContext().Roles.ToList();
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


        public JsonResult JsonDataForDatable()
        {
            String searchQuery = Request.Form.GetValues("search[value]").FirstOrDefault();
            int draw =Convert.ToInt32(Request.Form.GetValues("draw").FirstOrDefault());
            int start = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
            int perPage =Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();


            var returnDataArr = new object[0];
            int recordsTotal = 0;
            int filteredTotal = 0;
            List<object> jsonReturnRecords = new List<object>();
            using (var contxt = new DBContext())
            {
                var linQTotal = (from usrs in contxt.CompanyUsers
                           join usrls in contxt.UserRoles  on usrs.u_ID equals usrls.ur_u_ID into sub1
                           from t1 in sub1.DefaultIfEmpty()
                           join rols in contxt.Roles on t1.ur_rl_ID equals rols.Id into sub2
                           from t2 in sub2.DefaultIfEmpty(new Role { Name="Not Assign"})
                           where usrs.u_LoginName.Contains(searchQuery)  || t2.Name.Contains(searchQuery)                       
                           select new
                           {
                               UserName = ""+usrs.u_LoginName,
                               RoleName = t2.Name

                           }).ToList(); 

                recordsTotal = linQTotal.Count();

                var linQFiltered = linQTotal.OrderBy(x=>x.UserName).Skip(start).Take(perPage);
                filteredTotal = linQFiltered.Count();
                returnDataArr = new object[filteredTotal];
                jsonReturnRecords = linQFiltered.ToList<object>();

            }
           
            JsonResult json = Json(new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = recordsTotal, // calculated field
                recordsFiltered = recordsTotal, // calculated field
                data = jsonReturnRecords
            }, JsonRequestBehavior.AllowGet);


            return json;
        }

     





    }
}