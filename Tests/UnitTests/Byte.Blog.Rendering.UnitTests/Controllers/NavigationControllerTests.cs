using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Byte.Blog.Rendering.Controllers;
using Byte.Blog.Rendering.Models;
using Raven.Client;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests.Controllers
{
    public class NavigationControllerTests
    {
        [Fact]
        public void Menu_gets_home_page_as_first_result()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int pagesCount = 2;

            using (var navigationController = new NavigationController(store))
            {
                using (var session = store.OpenSession())
                {
                    PersistTestPages(session, pagesCount);

                    RavenControllerTestHelper.SetSessionOnController(navigationController, session);

                    var actionResult = navigationController.Menu();
                    var pageViewModels = ControllerTestHelper.GetModelInActionResult<IEnumerable<PageViewModel>>(actionResult);

                    var firstPageViewModel = pageViewModels.First();
                    Assert.Equal(Page.HomePage.Id, firstPageViewModel.Id);
                }
            }

            Mapper.Reset();
        }

        [Fact]
        public void Menu_gets_all_pages_in_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            int pagesCount = 2;

            using (var navigationController = new NavigationController(store))
            {
                using (var session = store.OpenSession())
                {
                    PersistTestPages(session, pagesCount);

                    RavenControllerTestHelper.SetSessionOnController(navigationController, session);
                    
                    var actionResult = navigationController.Menu();
                    var pageViewModels = ControllerTestHelper.GetModelInActionResult<IEnumerable<PageViewModel>>(actionResult);

                    for (int i = 0; i < pagesCount; i++)
                    {
                        Assert.True(pageViewModels.Any(vm => vm.Id == Page.IdPrefix + i));
                    }
                }
            }

            Mapper.Reset();
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
    }
}
