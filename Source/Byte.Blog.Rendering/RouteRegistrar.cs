using System.Web.Mvc;

namespace Byte.Blog.Rendering
{
    public class RouteRegistrar
    {
        private readonly AreaRegistrationContext context;

        public RouteRegistrar(AreaRegistrationContext context)
        {
            this.context = context;
        }

        public void RegisterRoutes()
        {
            this.RegisterNavigationRoutes();
            this.RegisterEntryRoutes();
            this.RegisterPageRoutes();
        }

        private void RegisterNavigationRoutes()
        {
            context.MapRoute(
                "Navigation-Menu",
                "navmenu",
                new { controller = "Navigation", action = "Menu" }
            );
        }

        private void RegisterEntryRoutes()
        {
            context.MapRoute(
                "Entries-EntrySlug",
                "entries/{entrySlug}",
                new { controller = "Entries", action = "ViewEntry", entrySlug = UrlParameter.Optional }
            );
        }

        private void RegisterPageRoutes()
        {
            context.MapRoute(
                "PageSlug",
                "{pageSlug}",
                new { controller = "Pages", action = "ViewPage", pageSlug = UrlParameter.Optional }
            );
        }
    }
}
