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
            RavenQueryStatistics stats;

            var results = this.session.Query<Entry>()
                .Statistics(out stats)
                .ToArray();

            var dashboardQueryModel = new EntryDashboardQueryModel()
            {
                PageNumber = 1,
                PageSize = EntriesPerPage, 
                TotalItems = stats.TotalResults
            };

            return this.View(dashboardQueryModel);
        }

        public ActionResult Fetch(EntryDashboardQueryModel queryModel)
        {
            var entryEditModels = this.session.Query<Entry>()
                .Where(e => !e.Deleted)
                .OrderByDescending(e => e.PublishedAtUtc)
                .Skip((queryModel.PageNumber - 1) * queryModel.PageSize)
                .Take(queryModel.PageSize)
                .ToList()
                .Select(this.CreateEntryEditModel);

            return this.Json(entryEditModels, JsonRequestBehavior.AllowGet);
        }

        private EntryEditModel CreateEntryEditModel(Entry entry)
        {
            var mapper = new EntryToEntryEditModelMapper(this.session);
            return mapper.Map(entry);
        }
    }
}
