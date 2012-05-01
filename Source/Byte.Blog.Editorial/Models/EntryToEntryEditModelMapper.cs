using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Byte.Blog.Content;
using MarkdownSharp;
using Raven.Client;

namespace Byte.Blog.Editorial.Models
{
    public class EntryToEntryEditModelMapper
    {
        private readonly IDocumentSession session;

        public EntryToEntryEditModelMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public EntryEditModel Map(Entry entry)
        {
            var entryEditModel = Mapper.Map<EntryEditModel>(entry);

            var markdown = new Markdown();

            entryEditModel.Preview = markdown.Transform(entry.Body);

            var page = this.GetPage(entry.PageId);

            entryEditModel.PageId = page.Id;
            entryEditModel.PageTitle = page.Title;
            entryEditModel.PageColor = page.HtmlColor;

            entryEditModel.PossiblePages = this.GetPossiblePages();

            return entryEditModel;
        }

        private Page GetPage(string pageId)
        {
            if (string.IsNullOrEmpty(pageId))
            {
                return Page.HomePage;
            }

            var page = this.session.Load<Page>(pageId);
            if (page == null)
            {
                return Page.HomePage;
            }

            return page;
        }

        private IEnumerable<Page> GetPossiblePages()
        {
            var pages = this.session.Query<Page>()
                .Where(page => page.Deleted == false)
                .ToList();

            pages.Insert(0, Page.HomePage);

            return pages;
        }
    }
}
