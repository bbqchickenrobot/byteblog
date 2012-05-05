using System.Web.Mvc;

namespace Byte.Blog.Editorial
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
            this.RegisterLoginRoutes();
            this.RegisterEntryDashboardRoutes();
            this.RegisterPageDashboardRoutes();
            this.RegisterEntryRoutes();
            this.RegisterPageRoutes();
            this.RegisterImportExportRoutes();
        }

        private void RegisterLoginRoutes()
        {
            //editorial/login
            this.context.MapRoute(
                "Editorial-Login",
                "editorial/auth/login",
                new { controller = "Login", action = "Login" });
        }

        private void RegisterEntryDashboardRoutes()
        {
            //editorial/entries/dashboard/{page}
            this.context.MapRoute(
                "Editorial-Entries-Dashboard",
                "editorial/entries/dashboard/{page}",
                new { controller = "EntriesDashboard", action = "Dashboard", page = UrlParameter.Optional });

            //editorial/entries/dashboard/{page}
            this.context.MapRoute(
                "Editorial-Entries-Fetch",
                "editorial/entries/fetch",
                new { controller = "EntriesDashboard", action = "Fetch" });
        }

        private void RegisterPageDashboardRoutes()
        {
            //editorial/pages/dashboard/{page}
            this.context.MapRoute(
                "Editorial-Pages-Dashboard",
                "editorial/pages/dashboard/{page}",
                new { controller = "PagesDashboard", action = "Dashboard", page = UrlParameter.Optional });

            //editorial/pages/dashboard/{page}
            this.context.MapRoute(
                "Editorial-Pages-Fetch",
                "editorial/pages/fetch",
                new { controller = "PagesDashboard", action = "Fetch" });
        }

        private void RegisterEntryRoutes()
        {
            //editorial/entries/edit/123
            this.context.MapRoute(
                "Editorial-Entries-Edit",
                "editorial/entries/edit/{entryId}",
                new { controller = "Entries", action = "Edit" });

            //editorial/entries/new
            this.context.MapRoute(
                "Editorial-Entries-New",
                "editorial/entries/new",
                new { controller = "Entries", action = "New" });

            //editorial/entries/delete
            this.context.MapRoute(
                "Editorial-Entries-Delete",
                "editorial/entries/delete/{entryId}",
                new { controller = "Entries", action = "Delete" });

            //editorial/entries/save
            this.context.MapRoute(
                "Editorial-Entries-Save",
                "editorial/entries/save",
                new { controller = "Entries", action = "Save" });
        }

        private void RegisterPageRoutes()
        {
            //editorial/pages/edit/123
            this.context.MapRoute(
                "Editorial-Pages-Edit",
                "editorial/pages/edit/{pageId}",
                new { controller = "Pages", action = "Edit" });

            //editorial/pages/new
            this.context.MapRoute(
                "Editorial-Pages-New",
                "editorial/pages/new",
                new { controller = "Pages", action = "New" });

            //editorial/pages/delete
            this.context.MapRoute(
                "Editorial-Pages-Delete",
                "editorial/pages/delete/{pageId}",
                new { controller = "Pages", action = "Delete" });

            //editorial/pages/save
            this.context.MapRoute(
                "Editorial-Pages-Save",
                "editorial/pages/save",
                new { controller = "Pages", action = "Save" });
        }

        private void RegisterImportExportRoutes()
        {
            //editorial/importexport/fromwordpress
            this.context.MapRoute(
                "Editorial-ImportExport-FromWordpress",
                "editorial/importexport/fromwordpress",
                new { controller = "ImportExport", action = "FromWordpress" });

            //editorial/importexport/importwordpressxml
            this.context.MapRoute(
                "Editorial-ImportExport-ImportWordpressXml",
                "editorial/importexport/importwordpressxml",
                new { controller = "ImportExport", action = "ImportWordpressXml" });
        }
    }
}
