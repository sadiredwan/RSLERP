using RSLERP.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RSLERP.Authorization
{
    public class SecurityAuthAuthorize : AuthorizeAttribute
    {

        public AccessLevel[] AccessLevels { get; set; }

        //public String AccessAction { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool haveAccess = true;
            var isAuthorized = base.AuthorizeCore(httpContext);

            string name = httpContext.User.Identity.Name;

            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string current_Cname = routeValues["controller"].ToString();
            string current_Aname = routeValues["action"].ToString();

            String userId = "" + RSLERPApplication.CurrentState().user_id;

            int user_id = Convert.ToInt32(userId);

            //List<RolePermission> rpLst = new List<RolePermission>();

            //using (var contxt = new DBContext())
            //{
            //    rpLst = (from ru in contxt.userRoles
            //             join rp in contxt.rolePermissions on ru.role_id equals rp.role_id
            //             join md in contxt.modules on rp.module_id equals md.Id
            //             where ru.user_id == user_id && md.ControllerName.ToLower() == current_Cname.ToLower()
            //             && md.Action.ToLower() == current_Aname.ToLower()
            //             select rp).ToList();
            //}

            //foreach (AccessLevel slvl in AccessLevels)
            //{
            //    switch (slvl)
            //    {
            //        case AccessLevel.View:
            //            haveAccess = rpLst.FindAll(x => x.Is_View == true).Count() > 0 ? true : false;
            //            break;
            //        case AccessLevel.Create:
            //            haveAccess = rpLst.FindAll(x => x.Is_Create == true).Count() > 0 ? true : false;
            //            break;
            //        case AccessLevel.Delete:
            //            haveAccess = rpLst.FindAll(x => x.Is_Delete == true).Count() > 0 ? true : false;
            //            break;
            //        case AccessLevel.Update:
            //            haveAccess = rpLst.FindAll(x => x.Is_Update == true).Count() > 0 ? true : false;
            //            break;

            //    }

            //    if (haveAccess)
            //    {
            //        break;
            //    }

            //}

            return haveAccess;


        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                       {
                                       { "action", "denied" },
                                       { "controller", "permission" }
                                       });
        }
    }

    public enum AccessLevel
    {
        View,
        Create,
        Delete,
        Update

    }
}