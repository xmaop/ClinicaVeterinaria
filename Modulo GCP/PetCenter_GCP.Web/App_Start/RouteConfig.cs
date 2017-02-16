using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetCenter_GCP.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", Id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DefaultCustom",
                url: "{controller}/{action}/{Parameter01}/{Parameter02}/{Parameter03}/{Parameter04}/{Parameter05}/{Parameter06}/{Parameter07}/{Parameter08}/{Parameter09}/{Parameter10}/{IdDialog}",
                defaults: new { controller = "Contenedor", action = "Index", Parameter01 = UrlParameter.Optional, Parameter02 = UrlParameter.Optional, Parameter03 = UrlParameter.Optional, Parameter04 = UrlParameter.Optional, Parameter05 = UrlParameter.Optional, Parameter06 = UrlParameter.Optional, Parameter07 = UrlParameter.Optional, Parameter08 = UrlParameter.Optional, Parameter09 = UrlParameter.Optional, Parameter10 = UrlParameter.Optional, IdDialog = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Contenedor",
            url: "{controller}/{action}/{ControllerName}/{ActionName}",
            defaults: new { controller = "Contenedor", action = "Index", ControllerName = UrlParameter.Optional, ActionName = UrlParameter.Optional }
            );
        }
    }
}
