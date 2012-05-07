using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using PressSharp;
using Raven.Client;

namespace Byte.Blog.Editorial
{
    public class WordpressImporter
    {
        private readonly IDocumentSession session;

        private readonly IList<Entry> importedEntries;
        private readonly IList<Page> importedPages; 

        public WordpressImporter(IDocumentSession session)
        {
            this.session = session;

            this.importedEntries = new List<Entry>();
            this.importedPages = new List<Page>();
        }

        public void ImportFromXml(XDocument document)
        {
            var parsedBlog = new PressSharp.Blog(document);

            var posts = parsedBlog.GetPosts();

            this.ImportPages(parsedBlog.Categories);
            this.session.SaveChanges();

            this.ImportPosts(posts);
            this.session.SaveChanges();
        }

        private void ImportPages(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                this.SaveCategoryAsPage(category);
            }
        }

        private void SaveCategoryAsPage(Category category)
        {
            var random = new Random();

            var page = new Page
            {
                Title = category.Name,
                Slug = category.Slug,
                HtmlColor = HtmlColor.AllColors[random.Next(0, HtmlColor.AllColors.Length)].HexColor,
                LastModifiedAtUtc = DateTimeOffset.UtcNow
            };

            this.session.Store(page);
        }

        private void ImportPosts(IEnumerable<Post> posts)
        {
            foreach (var post in posts)
            {
                this.SavePostAsEntry(post);
            }
        }

        public void SavePostAsEntry(Post post)
        {
            var entry = this.ConvertPostToEntry(post);

            this.session.Store(entry);
        }

        private Entry ConvertPostToEntry(Post post)
        {
            var entry = new Entry();

            entry.PageId = this.GetPageIdFromPost(post);
            entry.Body = post.Body;
            entry.LastModifiedAtUtc = DateTimeOffset.UtcNow;
            entry.Slug = post.Slug;
            entry.Title = post.Title;
            entry.Tags = new Collection<string>(post.Tags.Select(t => t.Slug).ToList());

            return entry;
        }

        private string GetPageIdFromPost(Post post)
        {
            var pageCategory = post.Categories.FirstOrDefault();
            if (pageCategory == null)
            {
                return Page.HomePage.Id;
            }

            var page = this.importedPages
                .FirstOrDefault(p => p.Slug == pageCategory.Slug);

            if (page == null)
            {
                return Page.HomePage.Id;
            }

            return page.Id;
        }

        public WordpressImportStatistics GetStatistics()
        {
            return new WordpressImportStatistics
            {
                EntriesCount = this.GetCountOfEntriesImported(),
                PagesCount = this.GetCountOfPagesImported(),
                TagsCount = this.GetCountOfTagsImported()
            };
        }

        private int GetCountOfEntriesImported()
        {
            return this.importedEntries.Count();
        }

        private int GetCountOfPagesImported()
        {
            return this.importedPages.Count();
        }

        private int GetCountOfTagsImported()
        {
            return this.importedEntries.SelectMany(e => e.Tags).Count();
        }
    }
}
