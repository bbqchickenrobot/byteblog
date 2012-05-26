using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Byte.Blog.Framework.Web
{
    public class MvcApplication : HttpApplication
    {
        public void Application_Start()
        {
            RegisterGlobalFilters();
            RegisterRoutes();

            var container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            var applicationState = new ApplicationState()
            {
                Container = container
            };

            AreaRegistration.RegisterAllAreas(applicationState);
        }

        private static void RegisterGlobalFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        public static EnvironmentType EnvironmentType
        {
            get
            {
                if(ConfigurationManager.AppSettings["Environment"] == "Release")
                {
                    return EnvironmentType.Production;
                }

                return EnvironmentType.Development;
            }
        }
    }
}
