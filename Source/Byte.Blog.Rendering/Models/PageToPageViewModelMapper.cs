using System.Linq;
using AutoMapper;
using Byte.Blog.Content;
using Raven.Client;
using Raven.Client.Linq;

namespace Byte.Blog.Rendering.Models
{
    public class PageToPageViewModelMapper
    {
        private readonly IDocumentSession session;

        public PageToPageViewModelMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public PageViewModel Map(Page page)
        {
            var pageViewModel = Mapper.Map<PageViewModel>(page);

            var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper();

            var query = this.session.Query<Entry>()
                .Where(e => e.PageId == page.Id && e.Published && !e.Deleted)
                .OrderByDescending(e => e.PublishedAtUtc);

            var entries = query.ToList();

            pageViewModel.Entries = entries
                .Select(entryToEntryViewModelMapper.Map);

            return pageViewModel;
        }
    }
}