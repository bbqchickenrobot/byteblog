using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using MarkdownSharp;
using Raven.Client;

namespace Byte.Blog.Rendering.Models
{
    public class EntryToEntryViewModelMapper
    {
        private readonly IDocumentSession session;
        private readonly UrlHelper urlHelper;

        public EntryToEntryViewModelMapper(
            IDocumentSession session,
            UrlHelper urlHelper)
        {
            this.session = session;
            this.urlHelper = urlHelper;
        }

        public EntryViewModel Map(Entry entry)
        {
            var entryViewModel = Mapper.Map<EntryViewModel>(entry);

            var pageViewModel = this.GetPageViewModel(entryViewModel.PageId);

            PopulatePageDetails(entryViewModel, pageViewModel);
            PopulateBody(entryViewModel);
            
            this.PopulateCanonicalUrl(entryViewModel);

            PopulateDisqusThread(entryViewModel, pageViewModel);

            return entryViewModel;
        }

        private PageViewModel GetPageViewModel(string pageId)
        {
            var pageToPageViewModelMapper = new PageToPageViewModelMapper(this.session, this.urlHelper);

            if (string.IsNullOrEmpty(pageId))
            {
                return pageToPageViewModelMapper.Map(Page.HomePage, false);
            }

            var page = this.session.Load<Page>(pageId);
            if (page == null)
            {
                return pageToPageViewModelMapper.Map(Page.HomePage, false);
            }

            return pageToPageViewModelMapper.Map(page, false);
        }

        private static void PopulatePageDetails(EntryViewModel entryViewModel, PageViewModel pageViewModel)
        {
            entryViewModel.PageSlug = pageViewModel.Slug;
        }

        private static void PopulateBody(EntryViewModel entryViewModel)
        {
            var markdown = new Markdown();

            entryViewModel.Body = markdown.Transform(entryViewModel.Body);
        }

        private void PopulateCanonicalUrl(EntryViewModel entryViewModel)
        {
            var canonicalUrlGenerator = new CanonicalUrlGenerator(this.urlHelper);

            entryViewModel.CanonicalUrl = canonicalUrlGenerator.FromRouteValues(
                "viewentry",
                "Entries",
                new
                {
                    pageSlug = entryViewModel.PageSlug,
                    entrySlug = entryViewModel.Slug
                });
        }

        private static void PopulateDisqusThread(EntryViewModel entryViewModel, PageViewModel pageViewModel)
        {
            entryViewModel.DisqusThread = new DisqusThreadViewModel
            {
                Shortname = pageViewModel.DisqusShortname,
                Identifier = entryViewModel.Id,
                Url = entryViewModel.CanonicalUrl
            };
        }
    }
}
