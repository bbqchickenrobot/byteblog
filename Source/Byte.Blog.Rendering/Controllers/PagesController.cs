using System.Linq;
using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Framework.Data;
using Byte.Blog.Rendering.Models;
using Raven.Client;

namespace Byte.Blog.Rendering.Controllers
{
    public class PagesController : RavenController
    {
        public PagesController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult ViewPage(string pageSlug)
        {
            Page page;

            if (string.IsNullOrEmpty(pageSlug))
            {
                page = Page.HomePage;
            }
            else
            {
                page = this.session.Query<Page>()
                    .FirstOrDefault(p => p.Slug == pageSlug);

                if (page == null)
                {
                    return new HttpNotFoundResult("No such page.");
                }
            }

            var entries = this.session.Query<Entry>()
                .Where(e => e.PageId == page.Id)
                .OrderByDescending(e => e.PublishedAtUtc)
                .ToList();

            var entryCollectionViewModel = new EntryCollectionViewModel();

            if (entries.Any())
            {
                entryCollectionViewModel.Entries = entries.Select(e => new EntryToEntryViewModelMapper().Map(e));
            }

            return this.View(entryCollectionViewModel);
        }
    }
}
