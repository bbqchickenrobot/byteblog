using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.UnitTests;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class EntriesControllerTests
    {
        [Fact]
        public void Save_persists_valid_entry_to_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string id = "foo/123";

            var store = new TestableStore();

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(entriesController, session);
                    entriesController.Save(new EntryEditModel() { Id = id, Title = "foo" });
                }
            }

            Assert.True(store.Contains(id));

            Mapper.Reset();
        }

        [Fact]
        public void Delete_sets_entry_deleted()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            var entry = new Entry
            {
                Id = "entries/123",
                Title = "foo"
            };

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    session.Store(entry);
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(entriesController, session);

                    entriesController.Delete(entry.Id);

                    var entryLoaded = session.Load<Entry>(entry.Id);
                    Assert.True(entryLoaded.Deleted);
                }
            }

            Mapper.Reset();
        }

        [Fact]
        public void Edit_pulls_entry_from_database_and_creates_viewmodel()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            var entry = new Entry
            {
                Id = "entries/123",
                Title = "foo"
            };

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    session.Store(entry);
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(entriesController, session);

                    string uniquePart = entry.Id.Replace(Entry.IdPrefix, "");

                    var actionResult = entriesController.Edit(uniquePart);
                    var entryEditModel = ControllerTestHelper.GetModelInActionResult<EntryEditModel>(actionResult);

                    Assert.Equal(entry.Title, entryEditModel.Title);
                }
            }

            Mapper.Reset();
        }
    }
}
