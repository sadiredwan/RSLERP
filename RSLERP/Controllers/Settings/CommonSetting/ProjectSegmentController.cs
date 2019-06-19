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
    public class ProjectSegmentController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Project Segment list
        /// </summary>
        /// <returns></returns>
        // GET: ProjectSegment
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            vmdl.VM_PROJECT_SEGMENTS = new DBContext().ProjectSegments.ToList();
            vmdl.VM_BUSINESS_SECTORS = new DBContext().BusinessSectors.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Project Segment 
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

            int psID = Convert.ToInt32(id);

            //pass model to view
            ProjectSegment mdlProjectSegment = new ProjectSegment();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Project Segment already exist
                if (new DBContext().ProjectSegments.ToList().FindAll(x => x.id == psID).Count > 0)
                {
                    //pass model to view with Project Segment info
                    mdlProjectSegment = new DBContext().ProjectSegments.Find(psID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_PROJECT_SEGMENT = mdlProjectSegment;
            }
            vmdl.VM_BUSINESS_SECTORS = new DBContext().BusinessSectors.ToList();
            return View(vmdl);
        }

        /// <summary>
        /// Store Project Segment 
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
                if (new DBContext().ProjectSegments.ToList().FindAll(x => x.id == vmdl.VM_PROJECT_SEGMENT.id).Count > 0)
                {
                    //Update Project Segment
                    //VM_PROJECT_SEGMENT.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.ProjectSegments.Attach(vmdl.VM_PROJECT_SEGMENT);
                        contxt.Entry(vmdl.VM_PROJECT_SEGMENT).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Project Segment
                    using (var contxt = new DBContext())
                    {
                        contxt.ProjectSegments.Add(vmdl.VM_PROJECT_SEGMENT);
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