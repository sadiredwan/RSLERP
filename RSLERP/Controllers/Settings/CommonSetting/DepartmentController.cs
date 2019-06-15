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

namespace RSLERP.Controllers.Settings {
    public class DepartmentController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Depart list
        /// </summary>
        /// <returns></returns>
        // GET: Department
        public ActionResult Index()
        {
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            vmdl.VM_DEPARTMENTS = new DBContext().Departments.ToList();

            return View(vmdl);
        }


        /// <summary>
        /// Create Page for Department 
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
            Department mdlDepartment = new Department();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Department already exist
                if (new DBContext().Departments.ToList().FindAll(x => x.id == dID).Count > 0)
                {
                    //pass model to view with Department info
                    mdlDepartment = new DBContext().Departments.Find(dID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_DEPARTMENT = mdlDepartment;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store Department 
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
                if (new DBContext().Departments.ToList().FindAll(x => x.id == vmdl.VM_DEPARTMENT.id).Count > 0)
                {
                   
                    //Update department
                    //updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Departments.Attach(vmdl.VM_DEPARTMENT);
                        contxt.Entry(vmdl.VM_DEPARTMENT).State = EntityState.Modified;
                        contxt.SaveChanges();
                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Department
                    using (var contxt = new DBContext())
                    {
                        contxt.Departments.Add(vmdl.VM_DEPARTMENT);
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