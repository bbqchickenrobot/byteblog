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
    public class PagesDashboardControllerTests
    {
        [Fact]
        public void Dashboard_fetch_crafts_viewmodels_from_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int pagesCount = 2;

            var pages = GetTestPages(pagesCount);
            SaveTestPages(store, pages);

            using (var pagesDashboardController = new PagesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(pagesDashboardController, session);

                    var queryModel = new PageDashboardQueryModel
                    {
                        PageNumber = 1,
                        PageSize = 20
                    };

                    var actionResult = pagesDashboardController.Fetch(queryModel);
                    var pageEditModels = GetPageEditModelsFromResult(actionResult);

                    foreach (var page in pages)
                    {
                        Assert.True(pageEditModels.Any(editModel => editModel.Id == page.Id));
                    }
                }
            }
        }

        private static IEnumerable<Page> GetTestPages(int number)
        {
            return Enumerable.Range(0, number)
                .Select(n =>
                    new Page
                    {
                        Id = "pages/" + n,
                        Title = "page " + n
                    });
        }

        private static void SaveTestPages(IDocumentStore store, IEnumerable<Page> pages)
        {
            using (var session = store.OpenSession())
            {
                foreach (var page in pages)
                {
                    session.Store(page);
                }
                session.SaveChanges();
            }
        }

        private static IEnumerable<PageEditModel> GetPageEditModelsFromResult(ActionResult actionResult)
        {
            var jsonResult = actionResult as JsonResult;
            if (jsonResult == null)
            {
                return Enumerable.Empty<PageEditModel>();
            }

            return jsonResult.Data as IEnumerable<PageEditModel>;
        }
    }
}
