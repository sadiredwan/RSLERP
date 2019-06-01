using RSLERP.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.DataManager.Entity;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager;
using System.Data.Entity;

namespace RSLERP.Controllers.SecurityNSettings.Settings
{
    public class ResourceEntryController : Controller
    {
        // GET: ResourceEntry
        public ActionResult Index()
        {

            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            string path = System.Environment.CurrentDirectory;

            List<s_Resource> lstresource = new List<s_Resource>();//new ModuleBLL().getAllResource();
            ViewBag.appmenus = new ModuleBLL().getApplicationMenu(User.Identity.Name);
            return View(lstresource);
            //return View(Globalpath.SecurityNSettings_Settings_ResourceEntryController+"index.cshtml",lstresource);
        }

        public JsonResult FindResource(String query)
        {
            List<AutoComplete> lstresource = new ModuleBLL().getAutoComplete(GlobalApplicationTableName.Resource, query);
            return Json(lstresource, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindResourceByID(String id)
        {
            List<s_Resource> resource = new ModuleBLL().getAllResource(id);
            return Json(resource.Count>0?resource[0].R_ResourceName:"", JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult ResourceMapping(String id,String Parent_ID,String appid)
        {
            List<s_Resource> lstresource = new ModuleBLL().getAllResource(id);
            if(lstresource.Count>0)
            {
                if (lstresource[0].R_Url.Contains("/"))
                {
                    String controller_name = lstresource[0].R_Url.Split('/')[0];
                    String action_name = lstresource[0].R_Url.Split('/')[1];

                    lstresource[0].Controller_Name = controller_name;
                    lstresource[0].Controller_Name = action_name;
                }
                else
                {
                    lstresource[0].Controller_Name = "";
                    lstresource[0].Controller_Name = "";
                }
                return PartialView(lstresource[0]);
            }
            else
            {

                appid = appid.Length < 2 ? "0" + appid : appid;
                return PartialView(new s_Resource {R_M_ID=appid,Parent_R_ID=(Parent_ID==null?"0":Parent_ID) });
            }
          
        }
        
        [HttpPost]
        public ActionResult ChangeResource(s_Resource model)
        {

            if (model.Controller_Name != "")
            {
                model.R_Url = model.Controller_Name + "/" + model.Action_Name;
            }
            if(model.parent_resource_id>0)
            {
                model.Parent_R_ID = ""+model.parent_resource_id;
            }
            if(Request.Form["R_DisplayName_id"] !=null)
            {
                string parentModeulID = Request.Form["R_DisplayName_id"].ToString();
                if(model.R_ID==0 && parentModeulID!="")
                {
                    model.R_ID = Convert.ToInt32(parentModeulID);
                    
                }
            }
            if (Request.Form["Is_Menu"] != null)
            {
                model.Is_Menu = Request.Form["Is_Menu"].ToString() == "on" ? true : false;
            }
            if (Request.Form["R_Status"] != null)
            {
                model.R_Status = Request.Form["R_Status"].ToString() == "on" ? true : false;
            }

             new ModuleBLL().ResourceInsert(model);
            HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES] = new ModuleBLL().getMenu();

            return RedirectToAction("Index");
        }


        public ActionResult delete(String id)
        {
            //Check if  not null
            if (id != null)
            {
                int dID = Convert.ToInt32(id);

                //Find the Employee 
                Resource dgMdl = new DBContext().Resources.Find(dID);


                //Delete The Employee
                using (var contxt = new DBContext())
                {
                    contxt.Entry(dgMdl).State = EntityState.Deleted;
                    contxt.SaveChanges();
                }
                
                HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES] = new ModuleBLL().getMenu();

            }

            return RedirectToAction("Index");

        }

    }
}