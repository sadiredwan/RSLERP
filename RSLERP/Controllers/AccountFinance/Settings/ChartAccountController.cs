using RSLERP.DataManager;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.AccountFinance.Settings
{
    public class ChartAccountController : Controller
    {
        ViewModel vmdl = new ViewModel();
        // GET: ChartAccount
        public ActionResult Index()
        {
            vmdl.VM_ROLEPERMISSIONS = new List<RolePermission>();
            List<AnFCOA> sscolParent = new DBContext().AnFCOAs.ToList();
            List<TreeMenu> lstTreeMenu = makeTreeMenuCOA(0, sscolParent);
            vmdl.VM_TREE_MENUS = lstTreeMenu;
            
            return View(vmdl);
        }

        public List<TreeMenu> makeTreeMenuCOA(long parentid, List<AnFCOA> allmenuLst)
        {
            List<TreeMenu> menuLst = new List<Models.TreeMenu>();
            List<AnFCOA> srchLstbyParentID = allmenuLst.FindAll(x => (x.AnFCOAId == null ? 0 : x.AnFCOAId) == parentid);
            foreach (AnFCOA moduleMdl in srchLstbyParentID)
            {
                TreeMenu treemenuMdl = new TreeMenu();
                treemenuMdl.Link_Name = moduleMdl.Code + " - " + moduleMdl.Particular;
                treemenuMdl.Link_Url = "" + moduleMdl.Id;

                treemenuMdl.childTreeLst = makeTreeMenuCOA((long)moduleMdl.Id, allmenuLst);
                menuLst.Add(treemenuMdl);
            }

            return menuLst;
        }


        public PartialViewResult PartialLoadCOA(String id)
        {
           

            return PartialView(vmdl);
        }
    }
}