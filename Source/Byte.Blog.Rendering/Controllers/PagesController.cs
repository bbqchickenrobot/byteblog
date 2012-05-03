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
            var pageFactory = new PageFactory(this.session);
            var page = pageFactory.CreateFromSlug(pageSlug);
            if (page == null)
            {
                return new HttpNotFoundResult("No such page.");
            }

            var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper();

            var entryViewModels = this.session.Query<Entry>()
                .Where(e => e.PageId == page.Id && e.Published)
                .OrderByDescending(e => e.PublishedAtUtc)
                .ToList()
                .Select(entryToEntryViewModelMapper.Map);

            var entryCollectionViewModel = new EntryCollectionViewModel
            {
                Entries = entryViewModels
            };

            return this.View(entryCollectionViewModel);
        }
    }
}
