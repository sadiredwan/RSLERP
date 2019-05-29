using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RSLERP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

     //       routes.MapRoute(
     //    "Security",
     //    "Security/{controller}/{action}/{id}",
     //    new { controller = "Test", action = "Index", id = UrlParameter.Optional },
     //    new[] { "RSLERP.Controllers.Security" }
     //);


     //       routes.MapRoute(
     //             "Home",
     //             "Home/{controller}/{action}/{id}",
     //             new { controller = "Home", action = "Index", id = UrlParameter.Optional },
     //             new[] { "RSLERP.Controllers.Home" }
     //         );





            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Start", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
