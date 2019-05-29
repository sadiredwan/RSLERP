using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.CommonLayout
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SubmitStatus(bool status, String message)
        {
            if (message != null)
            {
                ViewBag.status = status;
                ViewBag.message = message;
                return PartialView();
            }
            else
            {
                ViewBag.status = false;
                ViewBag.message = "";
                return PartialView();
            }
        }
    }
}