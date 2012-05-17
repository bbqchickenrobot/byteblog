using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Raven.Client;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests
{
    public class EntryFactoryTests
    {
        [Fact]
        public void Published_entry_in_database_is_successfully_returned_by_its_slug()
        {
            var testableStore = new TestableStore();

            string slug = "foo-slug";
            string title = "Foo Title";

            var testEntry = new Entry
            {
                Slug = slug,
                Title = title,
                Published = true
            };

            using (var session = testableStore.OpenSession())
            {
                session.Store(testEntry);
                session.SaveChanges();

                var entryReturned = GetEntryUsingFactory(session, slug);

                Assert.Equal(title, entryReturned.Title);
            }
        }

        [Fact]
        public void Non_published_entry_in_database_is_not_returned_by_its_slug()
        {
            var testableStore = new TestableStore();

            string slug = "foo-slug";

            var testEntry = new Entry
            {
                Slug = slug,
                Published = false
            };

            using (var session = testableStore.OpenSession())
            {
                session.Store(testEntry);
                session.SaveChanges();

                var entryReturned = GetEntryUsingFactory(session, slug);

                Assert.Null(entryReturned);
            }
        }

        [Fact]
        public void Deleted_entry_in_database_is_not_returned_by_its_slug()
        {
            var testableStore = new TestableStore();

            string slug = "foo-slug";

            var testEntry = new Entry
            {
                Slug = slug,
                Published = true,
                Deleted = true
            };

            using (var session = testableStore.OpenSession())
            {
                session.Store(testEntry);
                session.SaveChanges();

                var entryReturned = GetEntryUsingFactory(session, slug);

                Assert.Null(entryReturned);
            }
        }

        [Fact]
        public void Entry_not_in_database_returns_null()
        {
            var testableStore = new TestableStore();

            using (var session = testableStore.OpenSession())
            {
                var entryFactory = new EntryFactory(session);
                var entry = entryFactory.CreateFromSlug("non-existent");

                Assert.Null(entry);
            }
        }

        private static Entry GetEntryUsingFactory(IDocumentSession session, string slug)
        {
            var entryFactory = new EntryFactory(session);
            return entryFactory.CreateFromSlug(slug);
        }
    }
}
