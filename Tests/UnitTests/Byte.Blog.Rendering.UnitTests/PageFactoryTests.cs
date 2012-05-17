using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Raven.Client;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests
{
    public class PageFactoryTests
    {
        [Fact]
        public void Deleted_pages_are_not_returned()
        {
            var store = new TestableStore();

            string slug = "foo-bar-baz";

            var page = new Page
            {
                Id = "pages/123",
                Slug = slug,
                Deleted = true
            };

            using (var session = store.OpenSession())
            {
                session.Store(page);
                session.SaveChanges();

                var pageLoaded = GetPageForSlug(session, slug);

                Assert.Null(pageLoaded);
            }
        }

        [Fact]
        public void PageFactory_can_get_page_from_database_by_slug()
        {
            var store = new TestableStore();

            string slug = "foo-bar-baz";

            var page = new Page
            {
                Id = "pages/123",
                Slug = slug
            };

            using (var session = store.OpenSession())
            {
                session.Store(page);
                session.SaveChanges();

                var pageLoaded = GetPageForSlug(session, slug);

                Assert.Equal(page.Id, pageLoaded.Id);
            }
        }

        private static Page GetPageForSlug(IDocumentSession session, string slug)
        {
            var pageFactory = new PageFactory(session);
            return pageFactory.CreateFromSlug(slug);
        }
    }
}
