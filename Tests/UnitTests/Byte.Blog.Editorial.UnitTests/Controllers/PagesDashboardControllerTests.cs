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
        private const int DefaultQueryModelPageSize = 20;

        [Fact]
        public void Dashboard_fetch_crafts_viewmodels_from_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int pagesCount = 2;

            using (var pagesDashboardController = new PagesDashboardController(store))
            {
                using (var session = store.OpenSession())
                {
                    PersistTestPages(session, pagesCount);

                    RavenControllerTestHelper.SetSessionOnController(pagesDashboardController, session);

                    var queryModel = GetDefaultQueryModel();

                    var actionResult = pagesDashboardController.Fetch(queryModel);
                    var pageEditModels = GetPageEditModelsFromResult(actionResult);

                    for (int i = 0; i < pagesCount; i++)
                    {
                        Assert.True(pageEditModels.Any(editModel => editModel.Id == Page.IdPrefix + i));
                    }
                }
            }
        }

        private static void PersistTestPages(IDocumentSession session, int numberOfPages)
        {
            var entries = GetTestPages(numberOfPages);
            SaveTestPages(session, entries);
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

        private static void SaveTestPages(IDocumentSession session, IEnumerable<Page> pages)
        {
            foreach (var page in pages)
            {
                session.Store(page);
            }
            session.SaveChanges();
        }

        private static PageDashboardQueryModel GetDefaultQueryModel()
        {
            return new PageDashboardQueryModel
            {
                PageNumber = 1,
                PageSize = DefaultQueryModelPageSize
            };
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
