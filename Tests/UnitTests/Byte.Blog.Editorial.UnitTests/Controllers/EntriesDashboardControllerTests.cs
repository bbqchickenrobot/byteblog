using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.UnitTests;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class EntriesDashboardControllerTests
    {
        [Fact]
        public void Dashboard_fetch_crafts_viewmodels_from_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int entryCount = 2;

            var entries = this.GetTestEntries(entryCount);

            using (var entriesDashboardController = new EntriesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(entriesDashboardController, session);

                    foreach (var entry in entries)
                    {
                        session.Store(entry);
                    }
                    session.SaveChanges();

                    var actionResult = entriesDashboardController.Fetch(
                        new EntryDashboardQueryModel
                        {
                            PageNumber = 1,
                            PageSize = 20
                        });

                    var jsonData = (actionResult as System.Web.Mvc.JsonResult).Data;
                    var entryEditModels = jsonData as IEnumerable<EntryEditModel>;

                    foreach (var entry in entries)
                    {
                        Assert.True(entryEditModels.Any(editModel => editModel.Id == entry.Id));
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

            var entries = this.GetTestEntries(entryCount);

            using (var entriesDashboardController = new EntriesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(entriesDashboardController, session);

                    foreach (var entry in entries)
                    {
                        session.Store(entry);
                    }
                    session.SaveChanges();

                    var queryModel = new EntryDashboardQueryModel
                    {
                        PageNumber = 1,
                        PageSize = 20
                    };

                    var actionResult = entriesDashboardController.Fetch(
                        queryModel);

                    var jsonData = (actionResult as System.Web.Mvc.JsonResult).Data;
                    var entryEditModels = jsonData as IEnumerable<EntryEditModel>;

                    Assert.Equal(queryModel.PageSize, entryEditModels.Count());
                }
            }
        }

        private IEnumerable<Entry> GetTestEntries(int number)
        {
            return Enumerable.Range(0, number)
                .Select(n =>
                    new Entry
                    {
                        Id = "entries/" + n,
                        Title = "entry " + n
                    });
        }
    }
}
