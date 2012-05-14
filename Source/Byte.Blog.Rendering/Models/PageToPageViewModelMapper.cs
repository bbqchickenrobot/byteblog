using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Content;
using Raven.Client;
using Raven.Client.Linq;

namespace Byte.Blog.Rendering.Models
{
    public class PageToPageViewModelMapper
    {
        //TODO: move to configuration
        private const string DisqusShortname = "benlakey";

        private readonly IDocumentSession session;
        private readonly UrlHelper urlHelper;

        public PageToPageViewModelMapper(
            IDocumentSession session,
            UrlHelper urlHelper)
        {
            this.session = session;
            this.urlHelper = urlHelper;
        }

        public PageViewModel Map(Page page, bool includeEntries = true)
        {
            var pageViewModel = Mapper.Map<PageViewModel>(page);

            pageViewModel.DisqusShortname = DisqusShortname;

            if (includeEntries)
            {
                this.PopulateEntries(pageViewModel);
            }

            return pageViewModel;
        }

        private void PopulateEntries(PageViewModel pageViewModel)
        {
            var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(
                this.session,
                this.urlHelper);

            var query = this.session.Query<Entry>()
                .Where(e => e.PageId == pageViewModel.Id && e.Published && !e.Deleted)
                .OrderByDescending(e => e.PublishedAtUtc);

            var entries = query.ToList();

            pageViewModel.Entries = entries
                .Select(entryToEntryViewModelMapper.Map);
        }
    }
}