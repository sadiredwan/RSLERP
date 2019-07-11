using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.Global;
using RSLERP.Models;
using RSLERP.Authorization;
using RSLERP.DataManager.Entity;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager;

namespace RSLERP.Controllers.Home
{
    public class StartController : Controller
    {
        ViewModel vm = new ViewModel();
        ModuleBLL mdBll = new ModuleBLL();
        // GET: Start
        //[RSLERPAuthorize(AccessLevel = "User")]
        public ActionResult Index()
        {          
            if(TempData["ViewModel"]!=null)
            {
                vm= (ViewModel)TempData["ViewModel"];
            }
            vm.VM_MENUS = mdBll.getApplicationMenu(User.Identity.Name);
            string name = User.Identity.Name;

           // List<Menus> lstMenus = new ModuleBLL().getMenu(User.Identity.Name, "1", Applications.Security_Access);
            List<TreeMenu> lstTreeMenu = makeTreeMenu(0, vm.VM_MENUS);

            return View(vm);
           // return View(ActionViewResult.ActionView(this.ControllerContext), lstMenu);
        }

        public PartialViewResult UserAccountMenu()
        {
            //if (HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID] != null)
            //{
            //    String uID = HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            //    List<s_User> usrs = new List<s_User>();
            //    vm.VM_USER = (s_User)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS];// new SecurityUserAccessBLL().GetUserByUserID(uID).FirstOrDefault();
            //    vm.VM_COMPANY=(s_Company)HttpContext.Application[GLobalSessionName.GLOBAL_COMPANY_GROUP];
            //    //vm.VM_FINANCIALYEAR=(CmnTransactionalYears)HttpContext.Application[GLobalSessionName.GLOBAL_TRANSECTIONAL_YEAR];
            //}
            //else
            // {

                vm.VM_COMPANY_USER = new DBContext().CompanyUsers.Find(RSLERPApplication.CurrentState().user_id);
                vm.VM_COMPANE = new DBContext().Companies.Find(RSLERPApplication.CurrentState().company_id);
                vm.VM_GROUP = new DBContext().Groups.Find(RSLERPApplication.CurrentState().group_id);
                //vm.VM_USER = new s_User();
               // vm.VM_COMPANY = new s_Company();
                //vm.VM_FINANCIALYEAR = new CmnTransactionalYears();
            //}

            return PartialView(vm);
        }
        //public PartialViewResult UserMenu()
        //{
        //    if (HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID] != null)
        //    {
        //        String uID = HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
        //        List<s_User> usrs = new List<s_User>();
        //        vm.VM_USER = new SecurityUserAccessBLL().GetUserByUserID(uID).FirstOrDefault();
        //    }
        //    else
        //    {
        //        vm.VM_USER = new s_User();
        //    }
            
        //    return PartialView(vm);
        //}


        public List<TreeMenu> makeTreeMenu(int parentid, List<Menus> allmenuLst)
        {
            List<TreeMenu> menuLst = new List<Models.TreeMenu>();
            List<Menus> srchLstbyParentID = allmenuLst.FindAll(x => x.ParentModule_ID == parentid);
            foreach (Menus moduleMdl in srchLstbyParentID)
            {
                TreeMenu treemenuMdl = new TreeMenu();
                treemenuMdl.Link_Name = moduleMdl.Name;
                treemenuMdl.Link_Url = moduleMdl.url;
                treemenuMdl.isParent = moduleMdl.Is_Parent;
                treemenuMdl.childTreeLst = makeTreeMenu(moduleMdl.id, allmenuLst);
                menuLst.Add(treemenuMdl);
            }

            return menuLst;
        }
    }
}