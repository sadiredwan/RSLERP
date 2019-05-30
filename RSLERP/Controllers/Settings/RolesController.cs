using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.Global;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using static RSLERP.DataManager.Utility;

namespace RSLERP.Controllers.RoleManagement
{
    public class RolesController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Roles list
        /// </summary>
        /// <returns></returns>
        // GET: Role
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_ROLES = new DBContext().Roles.ToList();


            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
           
            String message = "";


            int dID = Convert.ToInt32(id);


            //pass model to view
            Role mdlRole = new Role();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Role already exist
                if (new DBContext().Roles.ToList().FindAll(x => x.Id == dID).Count > 0)
                {
                    //pass model to view with Role info
                    mdlRole = new DBContext().Roles.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_ROLE = mdlRole;
            }

            int levelStart = 1;
            int levelEnd = 2;

            for(int i= levelStart; i<= levelEnd; i++)
            {
                vmdl.VM_PRIORITY_LEVELS = new List<PriorityLevel>();
                vmdl.VM_PRIORITY_LEVELS.Add(new PriorityLevel { Id = i, Value = "" + i });
            }
            vmdl.VM_PRIORITY_LEVEL = new PriorityLevel();

            return View(vmdl);
        }

        /// <summary>
        /// Store Role 
        /// Create or Update
        /// </summary>
        /// <param name="RoleMdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
           
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().Roles.ToList().FindAll(x => x.Id == vmdl.VM_ROLE.Id).Count > 0)
                {
                    //Update Role
                    //RoleMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Roles.Attach(vmdl.VM_ROLE);
                        contxt.Entry(vmdl.VM_ROLE).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Role
                    using (var contxt = new DBContext())
                    {

                        contxt.Roles.Add(vmdl.VM_ROLE);
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