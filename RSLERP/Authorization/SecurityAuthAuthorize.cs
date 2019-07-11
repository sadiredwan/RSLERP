using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
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

            String path = "";

            string name = httpContext.User.Identity.Name;
            string id = "";

            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string current_Cname = routeValues["controller"].ToString();
            string current_Aname = routeValues["action"].ToString();

            if (routeValues.ContainsKey("id"))
                id= (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                id= HttpContext.Current.Request.QueryString["id"];

            String appID = "" + RSLERPApplication.CurrentState().id;

            path = current_Cname + "/Index";

            if (current_Aname.ToLower()=="load")
            {
                if (string.IsNullOrEmpty(id))
                {
                    //Create/Add Permission
                    if (AccessLevels.Contains(AccessLevel.Create))
                    {
                        haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.Create.ToString());
                    }
                }
                else
                {
                    //Update/Edit Permission
                    if (AccessLevels.Contains(AccessLevel.Update))
                    {
                        haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.Update.ToString());
                    }

                }
            }
            else if(current_Aname.ToLower()=="store")
            {
                //Create/Add Permission
                if (AccessLevels.Contains(AccessLevel.Create))
                {
                    haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.Create.ToString());
                }
                if (AccessLevels.Contains(AccessLevel.Update) && !haveAccess)
                {
                    haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.Update.ToString());
                }
            }
            else
            {
                if (AccessLevels.Contains(AccessLevel.View))
                {                
                    //View/Index Permission          
                    haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.View.ToString());
                }
                else if(AccessLevels.Contains(AccessLevel.Delete))
                {
                    //Delete Permission          
                    haveAccess = new SecurityUserAccessBLL().CHECK_SECURITY_ACCESS_MENUS(appID, path, AccessLevel.Delete.ToString());
                }
            }

            


            return haveAccess;


        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
           
            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                       {
                                       { "action", "denied" },
                                       { "controller", "access" }
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