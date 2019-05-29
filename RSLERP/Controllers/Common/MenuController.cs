using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using RSLERP.DataManager;
using System.Text;

namespace RSLERP.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MenuTree(String appID)
        {
            string name = "" + (System.Web.HttpContext.Current.Session["groupName"]!=null? System.Web.HttpContext.Current.Session["groupName"].ToString():"");
            var rd = ControllerContext.ParentActionViewContext.ParentActionViewContext.RouteData;
            var currentAction = rd.GetRequiredString("action");
            var currentController = rd.GetRequiredString("controller");
            String path = "" + currentController + "/" + currentAction;

            List<TreeMenu> lstChildMenu = new List<TreeMenu>();
            List<Menus> lstMenu = new List<Menus>();
           
            if(HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES]!=null && (List<Menus>)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES]!= null)
            {
                lstMenu = (List<Menus>)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES];
            }
            else
            {
                lstMenu = new ModuleBLL().getMenu();
                HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_MODULES] = lstMenu;
            }
            int applicationID = Convert.ToInt32(appID);
            //if(lstMenu.FindAll(x => x.url.ToLower() == path.ToLower()).Count>0)
            //{
            //    applicationID = lstMenu.Find(x => x.url.ToLower() == path.ToLower()).ApplicationModule_ID;
            //}
            //else
            //{
            //    path = "" + currentController + "/Index";
            //    try
            //    {
            //        applicationID = lstMenu.Find(x => x.url.ToLower() == path.ToLower()).ApplicationModule_ID;
            //    }catch
            //    {

            //    }
            //}
           
          

            lstMenu = lstMenu.FindAll(x => x.ApplicationModule_ID == applicationID);

            // List<Menu> lstMenu = new ModuleBLL().getMenu(User.Identity.Name,  Applications.Security_Access,"01", path);
            List<TreeMenu> lstTreeMenu = makeTreeMenu(0, lstMenu);    
            String menuString= ListTreeUlLi(0, lstMenu);

            ViewBag.MenuUlLi = menuString;

            return PartialView("~/Views/Partials/Menu/TreeMenu.cshtml", lstTreeMenu);
        }

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


        public String ListTreeUlLi(int parentid, List<Menus> allmenuLst)
        {
            var rd = ControllerContext.ParentActionViewContext.ParentActionViewContext.RouteData;
            var currentAction = rd.GetRequiredString("action");
            var currentController = rd.GetRequiredString("controller");
            String path = "" + currentController + "/" + currentAction;
            List<Menus> lstSelectNode = new List<Menus>();
           int crrentPathModuleID =  (allmenuLst.FindAll(x => x.url.ToLower() == path.ToLower()).Count > 0 ? allmenuLst.Find(x => x.url.ToLower() == path.ToLower()).id : 0);
            if (crrentPathModuleID != 0)
            {
               lstSelectNode = GetSelectedTreeNode(crrentPathModuleID, allmenuLst);
            }
            else
            {
                path = "" + currentController + "/Index";
                crrentPathModuleID = (allmenuLst.FindAll(x => x.url.ToLower() == path.ToLower()).Count > 0 ? allmenuLst.Find(x => x.url.ToLower() == path.ToLower()).id : 0);
                if (crrentPathModuleID != 0)
                {
                    lstSelectNode = GetSelectedTreeNode(crrentPathModuleID, allmenuLst);
                }
            }

            StringBuilder sb = new StringBuilder();
          
            List<TreeMenu> menuLst = new List<Models.TreeMenu>();
            List<Menus> srchLstbyParentID = allmenuLst.FindAll(x => x.ParentModule_ID == parentid && x.is_menu==true).OrderBy(x=>x.Menu_Order).ToList();

            foreach (Menus moduleMdl in srchLstbyParentID)
            {
                //int folderID = -1;
                //int linkid = -1;
                //if (allmenuLst.FindAll(x => x.url == path).Count>0)
                //    folderID=  allmenuLst.Find(x => x.url == path).ParentModule_ID;
                bool isFolderactive = false;
                //if(folderID==moduleMdl.id)
                //{
                //    isFolderactive = true;
                //}
                bool isLinkActive = false;


                //if(allmenuLst.FindAll(x => x.url == path).Count>0)
                //    linkid= allmenuLst.Find(x => x.url == path).id;
                //if (linkid == moduleMdl.id)
                //{
                //    isLinkActive = true;
                //}

                isFolderactive = lstSelectNode.FindAll(x => x.id == moduleMdl.id && x.Is_Parent==true).Count > 0 ? true : false;

                isLinkActive = lstSelectNode.FindAll(x => x.id == moduleMdl.id && x.Is_Parent == false).Count > 0 ? true : false;

                string fa_icon = moduleMdl.icon;
                if(string.IsNullOrEmpty(fa_icon))
                {
                    fa_icon = "fa fa-share";
                }

                if (allmenuLst.FindAll(x => x.ParentModule_ID == moduleMdl.id && x.is_menu==true).Count > 0)
                {                   
                    if (isFolderactive)
                        sb.Append("<li class=\"active treeview\">");
                    else
                        sb.Append("<li class=\"treeview\">");

                    sb.Append(@"<a href='/#'>
                            <i class='"+moduleMdl.icon+@"'></i> <span>" + moduleMdl.Name + @"</span>
                            <span class='pull-right-container'>
                                <i class='fa fa-angle-left pull-right'></i>
                            </span>
                            </a>");        
                                
                    sb.Append("<ul class=\"treeview-menu\">");
                    sb.Append(ListTreeUlLi(moduleMdl.id, allmenuLst));
                    sb.Append("</ul>");
                    sb.Append("</li>");
                }
                else
                {
                    if(isLinkActive)
                        sb.Append("<li class=\"active\">");
                    else
                        sb.Append("<li>");
                    sb.Append("<a href=\"/"+ moduleMdl .url+ "\"><i class=\""+ fa_icon + "\"></i><span>" + moduleMdl.Name + "</span></a>");
                    sb.Append("</li>");
                }
               
            }

          

          
            return sb.ToString();


             
        }


        public List<Menus> GetSelectedTreeNode(int id, List<Menus> allmenuLst)
        {
            List<Menus> lstMenu = new List<Menus>();
            Menus selectNode = new Menus();
            selectNode.id = id;
            int parentID = allmenuLst.Find(x => x.id == id).ParentModule_ID;
            selectNode.Name = allmenuLst.Find(x => x.id == id).Name;
            bool isChild = false;
            isChild = allmenuLst.FindAll(x => x.ParentModule_ID == id && x.is_menu==true).Count > 0 ? false : true;
            if(isChild)
            {
                selectNode.Is_Parent = false;
            }
            else
            {
                selectNode.Is_Parent = true;
            }

            if(parentID !=0)
            lstMenu = GetSelectedTreeNode(parentID, allmenuLst);

            lstMenu.Add(selectNode);

            return lstMenu;
        }


        public JsonResult TreeView(String appID,String treeName)
        {
            List<Menus> lstMenu = new ModuleBLL().getAllMenuByApplicationID(appID);
            List<TreeView> lstTreeMenu = makeTreeView(0, lstMenu);

            return Json(lstTreeMenu, JsonRequestBehavior.AllowGet);
        }


        public List<TreeView> makeTreeView(int parentid, List<Menus> allmenuLst)
        {
            List<TreeView> menuLst = new List<Models.TreeView>();
            List<Menus> srchLstbyParentID = allmenuLst.FindAll(x => x.ParentModule_ID == parentid);
            foreach (Menus moduleMdl in srchLstbyParentID)
            {
                TreeView treemenuMdl = new TreeView();
                treemenuMdl.text = moduleMdl.Name;
                treemenuMdl.href = ""+moduleMdl.id;
                //treemenuMdl.tags[0] = "4";
                treemenuMdl.nodes= makeTreeView(moduleMdl.id, allmenuLst).Count>0? makeTreeView(moduleMdl.id, allmenuLst):null;
                menuLst.Add(treemenuMdl);
            }

            return menuLst;
        }
    }
}