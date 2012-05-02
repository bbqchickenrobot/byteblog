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
    public class EntriesDashboardController : RavenController
    {
        private const int EntriesPerPage = 10;

        public EntriesDashboardController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult Dashboard()
        {
            var dashboardQueryModel = new EntryDashboardQueryModel()
            {
                PageNumber = 1,
                PageSize = EntriesPerPage
            };

            return this.View(dashboardQueryModel);
        }

        public ActionResult Fetch(EntryDashboardQueryModel queryModel)
        {
            var mapper = new EntryToEntryEditModelMapper(this.session);

            var entryEditModels = this.session.Query<Entry>()
                .Skip((queryModel.PageNumber - 1) * queryModel.PageSize)
                .Take(queryModel.PageSize)
                .ToList()
                .Select(mapper.Map);

            return this.Json(entryEditModels, JsonRequestBehavior.AllowGet);
        }
    }
}
