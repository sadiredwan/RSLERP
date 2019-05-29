using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RSLERP.Authorization
{
    public class RSLERPAuthorize : AuthorizeAttribute
    {

        public String[] AccessLevels { get; set; }

        public String AccessAction { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);

            string name = httpContext.User.Identity.Name;
            
            if (AccessLevels.Contains("Managerer"))
            {
                return true;
            }
            else
            {
                return false;
            }

          
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
            filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary
                                       {
                                       { "action", "Nopermission" },
                                       { "controller", "account" }
                                       });
        }
    }
}