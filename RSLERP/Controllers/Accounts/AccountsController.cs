using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.Global;
using RSLERP.Models;
namespace RSLERP.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            //Only for test purpose
            List<CustomTest> ctlst = new List<Models.CustomTest>();

            return View(ActionViewResult.ActionView(this.ControllerContext),ctlst);
        }


        public ActionResult Submit(List<CustomTest> model)
        {
            return RedirectToAction("Index");
        }
    }
}