using RSLERP.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.SupplyChain
{
    public class SupplyChainController : Controller
    {
        // GET: SupplyChain
        public ActionResult Index()
        {
            return View(Globalpath.SupplyChain_SupplyChainController+"index.cshtml");
        }
    }
}