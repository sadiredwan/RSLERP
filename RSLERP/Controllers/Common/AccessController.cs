using RSLERP.DataManager;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.Common
{
    public class AccessController : Controller
    {
        ViewModel vmdl = new ViewModel();
        // GET: Permission
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult denied()
        {
            vmdl.CommitStatus = false;
            vmdl.SaveChangeMessage = "You donot have permission to access this part,Please Contact wit your administrator";
            TempData["ViewModel"] = vmdl;

            return RedirectToAction("Index", "start");
        }
    }
}