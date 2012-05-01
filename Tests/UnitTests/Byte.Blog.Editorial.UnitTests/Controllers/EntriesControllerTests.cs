using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Editorial.Models;
using Raven.Client;
using Raven.Client.Embedded;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class EntriesControllerTests
    {
        [Fact]
        public void Save_persists_valid_entry_to_database()
        {
            WireUpMappings();

            var store = GetTestStore();

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    SetSessionOnController(entriesController, session);
                    entriesController.Save(new EntryEditModel() { Title = "foo" });
                }
            }

            AssertExistsInStore(store, e => e.Title == "foo");
        }

        [Fact]
        public void Delete_sets_entry_deleted()
        {
            WireUpMappings();

            var store = GetTestStore();

            InsertTestData(store, 1);

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    var entry = session.Query<Entry>()
                        .FirstOrDefault(e => e.Title == "foo 0");

                    SetSessionOnController(entriesController, session);

                    Assert.False(entry.Deleted);

                    entriesController.Delete(entry.Id);

                    Assert.True(entry.Deleted);
                }
            }
        }

        //[Fact]
        //public void Dashboard_crafts_viewmodel_with_entries_from_database()
        //{
        //    WireUpMappings();

        //    var store = GetTestStore();

        //    InsertTestData(store, 1);

        //    using (var entriesController = new EntriesController(store))
        //    {
        //        using (var session = store.OpenSession())
        //        {
        //            SetSessionOnController(entriesController, session);

        //            var actionResult = entriesController.Dashboard();
        //            var model = GetModelInActionResult<EntryDashboardViewModel>(actionResult);
                    
        //            Assert.Equal(1, model.Entries.Count());
        //            Assert.Equal("foo 0", model.Entries.First().Title);
        //        }
        //    }
        //}

        //[Fact]
        //public void Dashboard_uses_pagination_to_return_limited_results()
        //{
        //    WireUpMappings();

        //    var store = GetTestStore();

        //    const int maxFromPagination = 10;

        //    InsertTestData(store, maxFromPagination + 1);

        //    using (var entriesController = new EntriesController(store))
        //    {
        //        using (var session = store.OpenSession())
        //        {
        //            SetSessionOnController(entriesController, session);

        //            var actionResult = entriesController.Dashboard();
        //            var model = GetModelInActionResult<EntryDashboardViewModel>(actionResult);

        //            Assert.Equal(maxFromPagination, model.Entries.Count());
        //        }
        //    }
        //}

        [Fact]
        public void Edit_pulls_entry_from_database_and_creates_viewmodel()
        {
            WireUpMappings();

            var store = GetTestStore();

            InsertTestData(store, 1);

            using (var entriesController = new EntriesController(store))
            {
                using (var session = store.OpenSession())
                {
                    SetSessionOnController(entriesController, session);

                    var entry = session.Query<Entry>()
                        .FirstOrDefault(e => e.Title == "foo 0");

                    string uniquePart = entry.Id.Replace(Entry.IdPrefix, "");

                    var actionResult = entriesController.Edit(uniquePart);
                    var model = GetModelInActionResult<EntryEditModel>(actionResult);

                    Assert.Equal("foo 0", model.Title);
                }
            }
        }

        private static void InsertTestData(IDocumentStore store, int count)
        {
            using (var session = store.OpenSession())
            {
                for (int i = 0; i < count; i++)
                {
                    var entry = new Entry()
                    {
                        Title = "foo " + i
                    };

                    session.Store(entry);
                    session.SaveChanges();
                }
            }
        }

        private static T GetModelInActionResult<T>(ActionResult actionResult) where T : class
        {
            var viewResultBase = actionResult as ViewResultBase;
            if (viewResultBase == null)
            {
                return null;
            }

            return viewResultBase.ViewData.Model as T;
        }

        private static void SetSessionOnController(
            EntriesController entriesController,
            IDocumentSession session)
        {
            typeof(EntriesController).BaseType
                .GetProperty("session", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(entriesController, session, null);
        }

        private static void AssertExistsInStore(
            IDocumentStore store,
            Expression<Func<Entry, bool>> expression)
        {
            using (var session = store.OpenSession())
            {
                var foo = session.Query<Entry>()
                    .FirstOrDefault(expression);

                Assert.NotNull(foo);
            }
        }

        private static void WireUpMappings()
        {
            Mapper.CreateMap<Entry, EntryEditModel>();
            Mapper.CreateMap<EntryEditModel, Entry>();
        }

        private static IDocumentStore GetTestStore()
        {
            var store = new EmbeddableDocumentStore()
            {
                RunInMemory = true,
            };

            store.Initialize();

            return store;
        }
    }
}
