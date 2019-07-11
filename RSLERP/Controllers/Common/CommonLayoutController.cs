using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Global;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.CommonLayout
{
    public class CommonLayoutController : Controller
    {
        ViewModel vm = new ViewModel();
        ModuleBLL mdBll = new ModuleBLL();
        // GET: CommonLayout
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Footer(String layoutID)
        {
            if (layoutID == null || layoutID == "inner")
            {
                ViewBag.FooterVersion = ConfigurationManager.AppSettings["FooterVersion"].ToString();
                ViewBag.FooterText = ConfigurationManager.AppSettings["FooterText"].ToString().Replace("2017 CompanyName", DateTime.Now.Year.ToString()+" Enterprise Resource Planning System by raj IT Solutions Ltd.");
                return PartialView(Globalpath.commonlayoutpath_path + "Footer.cshtml");
            }
            else if (layoutID == "start")
            {
                ViewBag.FooterVersion = ConfigurationManager.AppSettings["FooterVersion"].ToString();
                ViewBag.FooterText = ConfigurationManager.AppSettings["FooterText"].ToString().Replace("2017 CompanyName", DateTime.Now.Year.ToString() + " Enterprise Resource Planning System by raj IT Solutions Ltd.");
                return PartialView(Globalpath.commonlayoutpath_path + "Footer.cshtml");
            }
            else
            {
                ViewBag.FooterVersion = ConfigurationManager.AppSettings["FooterVersion"].ToString();
                ViewBag.FooterText = ConfigurationManager.AppSettings["FooterText"].ToString().Replace("2017 CompanyName", DateTime.Now.Year.ToString() + " Enterprise Resource Planning System by raj IT Solutions Ltd.");
                return PartialView(Globalpath.commonlayoutpath_path + "Footer.cshtml");
            }
        }

        public PartialViewResult LeftSideBar(String applicationid)
        {
            vm.VM_MODULE = new s_Modules();
            vm.VM_MODULE.m_ID = applicationid;
            vm.VM_COMPANY_USER = new DBContext().CompanyUsers.Find(RSLERPApplication.CurrentState().user_id);
          
            return PartialView(vm);
        }


        public PartialViewResult UserAccountMenu()
        {
            //if (HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS] != null)
            //{
            //    vm.VM_USER = (s_User)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS];
            //}
            //else
            //{
            vm.VM_COMPANY_USER = new DBContext().CompanyUsers.Find(RSLERPApplication.CurrentState().user_id);
            vm.VM_COMPANE = new DBContext().Companies.Find(RSLERPApplication.CurrentState().CompanyId);
            vm.VM_GROUP = new DBContext().Groups.Find(RSLERPApplication.CurrentState().group_id);
           // }

            return PartialView(vm);
        }

        public PartialViewResult CurrentUserInfo()
        {
            List<s_User> usrs = new List<s_User>();
            //List<s_User> lstUser = new SecurityUserAccessBLL().GetuserbyLoginName(User.Identity.Name);
            return PartialView(Globalpath.commonlayoutpath_path + "CurrentUserInfo.cshtml", usrs[0]);
        }

        public PartialViewResult PageTitle()
        {
            ViewBag.ERPName = ConfigurationManager.AppSettings[GlobalRSLERP_App_Key.ERPName].ToString();
            var rd = ControllerContext.ParentActionViewContext.RouteData;
            var currentAction = rd.GetRequiredString("action");
            var currentController = rd.GetRequiredString("controller");
            ViewBag.PageName = currentController+ (currentAction=="Index"?"":"-"+currentAction);

            return PartialView(Globalpath.commonlayoutpath_path + "PageTitle.cshtml");
        }

        public PartialViewResult SubmitStatus(bool status,String message)
        {
            if (message != null)
            {
                ViewBag.status = status;
                ViewBag.message = message;
                return PartialView(ActionViewResult.ActionView(this.ControllerContext));
            }
            else
            {
                ViewBag.status =false;
                ViewBag.message = "";
                return PartialView(ActionViewResult.ActionView(this.ControllerContext));
            }
        }

    }
}