using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.Global;
using RSLERP.Models;
using RSLERP.DataManager.Entity;
using RSLERP.DataManager.BLL;
using System.Data.SqlClient;
using System.Configuration;

namespace RSLERP.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //List<tbl_User> lstUser = new SecurityUserAccessBLL().GetUser();
            return View(ActionViewResult.ActionView(this.ControllerContext));
        }

        public PartialViewResult Navigations()
        {         
            return PartialView("_NavigationMenu");
        }


        public PartialViewResult UserMenu()
        {
            return PartialView("UserMenu");
        }
            

        //public PartialViewResult PageHeader()
        //{
        //    var rd = ControllerContext.ParentActionViewContext.RouteData;
        //    var currentAction = rd.GetRequiredString("action");
        //    var currentController = rd.GetRequiredString("controller");
        //    ViewBag.BreadCrum = currentController + " > " + currentAction;
        //    return PartialView(new BreadCrum { firstname = currentController, secondname = currentAction });
        //}


    }
}