using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
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
            }

            using (var session = store.OpenSession())
            {
                var pageFactory = new PageFactory(session);
                var pageLoaded = pageFactory.CreateFromSlug(slug);

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
            }

            using (var session = store.OpenSession())
            {
                var pageFactory = new PageFactory(session);
                var pageLoaded = pageFactory.CreateFromSlug(slug);

                Assert.Equal(page.Id, pageLoaded.Id);
            }
        }
    }
}
