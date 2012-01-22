using System.Web.Mvc;
using System.Web.Routing;

namespace Domain2HostCMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "LogOn", // Route name
                "LogOn", // URL with parameters
                new { controller = "Account", action = "Logon" }
            );

            routes.MapRoute(
                "LogOff", // Route name
                "LogOff", // URL with parameters
                new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                "LogIn", // Route name
                "LogIn", // URL with parameters
                new { controller = "Account", action = "Logon" }
            );

            routes.MapRoute(
                "LogOut", // Route name
                "LogOut", // URL with parameters
                new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                "Register", // Route name
                "Register", // URL with parameters
                new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                "Pages", // Route name
                "Pages/{pid}", // URL with parameters
                new { controller = "Home", action = "Pages", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Controlpanel", // Route name
                "Controlpanel", // URL with parameters
                new { controller = "Controlpanel", action = "Index" }
            );

            routes.MapRoute(
                "Photo", // Route name
                "Photo/{pid}", // URL with parameters
                new { controller = "Home", action = "Photo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EditPage", // Route name
                "EditPage/{pid}", // URL with parameters
                new { controller = "Controlpanel", action = "EditPage", pid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeletePage", // Route name
                "DeletePage/{pid}", // URL with parameters
                new { controller = "Controlpanel", action = "DeletePage", pid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EditMenu", // Route name
                "EditMenu/{mid}", // URL with parameters
                new { controller = "Controlpanel", action = "EditMenu", mid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteMenu", // Route name
                "DeleteMenu/{mid}", // URL with parameters
                new { controller = "Controlpanel", action = "DeleteMenu", mid = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteImage", // Route name
                "DeleteImage/{mid}", // URL with parameters
                new { controller = "Controlpanel", action = "DeleteImage", mid = UrlParameter.Optional }
            );

            //routes.MapRoute("Default", "{controller}/{action}/{pid}", new { controller = "Home", action = "Pages", pid = "Index" } // Parameter defaults
            //routes.MapRoute("Default", "{controller}/{action}/{pid}", new { controller = "Controlpanel", action = "AddPage", pid = "Index" } // Parameter defaults
            //routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
            //routes.MapRoute("Default", "{controller}/{action}", new { controller = "Profiles", action = "Employer" });
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Controlpanel", action = "Index" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}