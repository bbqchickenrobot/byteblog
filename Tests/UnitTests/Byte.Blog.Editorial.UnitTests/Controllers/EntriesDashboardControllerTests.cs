using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.UnitTests;
using Raven.Client;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class EntriesDashboardControllerTests
    {
        private const int DefaultQueryModelPageSize = 20;

        [Fact]
        public void Dashboard_fetch_crafts_viewmodels_from_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int entryCount = 2;

            using (var entriesDashboardController = new EntriesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    PersistTestEntries(session, entryCount);

                    RavenControllerTestHelper.SetSessionOnController(entriesDashboardController, session);

                    var queryModel = GetDefaultQueryModel();

                    var actionResult = entriesDashboardController.Fetch(queryModel);
                    var entryEditModels = GetEntryEditModelsFromResult(actionResult);

                    for(int i = 0; i < entryCount; i++)
                    {
                        Assert.True(entryEditModels.Any(editModel => editModel.Id == Entry.IdPrefix + i));
                    }
                }
            }
        }

        [Fact]
        public void Dashboard_fetch_paginates_results()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int entryCount = 30;

            using (var entriesDashboardController = new EntriesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    PersistTestEntries(session, entryCount);

                    RavenControllerTestHelper.SetSessionOnController(entriesDashboardController, session);

                    var queryModel = GetDefaultQueryModel();

                    var actionResult = entriesDashboardController.Fetch(queryModel);
                    var entryEditModels = GetEntryEditModelsFromResult(actionResult);

                    Assert.Equal(queryModel.PageSize, entryEditModels.Count());
                }
            }
        }

        private static void PersistTestEntries(IDocumentSession session, int numberOfEntries)
        {
            var entries = GetTestEntries(numberOfEntries);
            SaveTestEntries(session, entries);
        }

        private static IEnumerable<Entry> GetTestEntries(int number)
        {
            return Enumerable.Range(0, number)
                .Select(n =>
                    new Entry
                    {
                        Id = "entries/" + n,
                        Title = "entry " + n
                    });
        }

        private static void SaveTestEntries(IDocumentSession session, IEnumerable<Entry> entries)
        {
            foreach (var entry in entries)
            {
                session.Store(entry);
            }
            session.SaveChanges();
        }

        private static EntryDashboardQueryModel GetDefaultQueryModel()
        {
            return new EntryDashboardQueryModel
            {
                PageNumber = 1,
                PageSize = DefaultQueryModelPageSize
            };
        }

        private static IEnumerable<EntryEditModel> GetEntryEditModelsFromResult(ActionResult actionResult)
        {
            var jsonResult = actionResult as JsonResult;
            if (jsonResult == null)
            {
                return Enumerable.Empty<EntryEditModel>();
            }

            return jsonResult.Data as IEnumerable<EntryEditModel>;
        }
    }
}
