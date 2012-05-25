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
            this.RegisterSidebar();
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
                "{pageSlug}/{entrySlug}",
                new { controller = "Entries", action = "ViewEntry" }
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

        private void RegisterSidebar()
        {
            context.MapRoute(
                "Sidebar",
                "sidebar",
                new { controller = "Sidebar", action = "Sidebar" }
            );
        }
    }
}
