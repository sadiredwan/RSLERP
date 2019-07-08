
using RSLERP.DataManager;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace RSLERP
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public const string CACHE_KEY_CHART_OF_ACCOUNT = "CACHEKEYCHARTOFACCOUNT";
        public const string CACHE_KEY_TRANSACTIONAL_ACCOUNT = "CACHEKEYTRANSACTIONALACCOUNT";
        public const string CACHE_KEY_BANK_ACCOUNT = "CACHEKEYBANKACCOUNT";
        public const string CACHE_KEY_CASH_ACCOUNT = "CACHEKEYCASHACCOUNT";

        private static HttpRuntime _httpRuntime;
        public static string dataConnString = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);         
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //if (Application[GLobalSessionName.GLOBAL_SESSION_USERID] == null)
            //{
            //    FormsAuthentication.SignOut();
            //    Response.Redirect("/Account/Signout");
            //}
            // Response.Redirect("/Account/Signout");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                int appID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
   

                if (new DBContext().ApplicationStates.Where(x => x.id == appID && x.status == true).Count() < 1)
                    {
                        FormsAuthentication.SignOut();
                    }
                
              
            }
        }

    }
}
