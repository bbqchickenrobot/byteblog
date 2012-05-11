using System.Linq;
using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.Data;
using Raven.Client;
using Raven.Client.Linq;

namespace Byte.Blog.Editorial.Controllers
{
    [Authorize]
    public class PagesDashboardController : RavenController
    {
        private const int PagesPerPage = 10;

        public PagesDashboardController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult Dashboard()
        {
            RavenQueryStatistics stats;

            var results = this.session.Query<Page>()
                .Statistics(out stats)
                .ToArray();

            var dashboardQueryModel = new PageDashboardQueryModel()
            {
                PageNumber = 1,
                PageSize = PagesPerPage,
                TotalItems = stats.TotalResults
            };

            return this.View(dashboardQueryModel);
        }

        public ActionResult Fetch(PageDashboardQueryModel queryModel)
        {
            var mapper = new PageToPageEditModelMapper(this.session);

            var pages = this.session.Query<Page>()
                .Skip((queryModel.PageNumber - 1) * queryModel.PageSize)
                .Take(queryModel.PageSize)
                .ToList();

            pages.Insert(0, Page.HomePage);

            var pageEditModels = pages
                .Select(mapper.Map);

            return this.Json(pageEditModels, JsonRequestBehavior.AllowGet);
        }
    }
}
