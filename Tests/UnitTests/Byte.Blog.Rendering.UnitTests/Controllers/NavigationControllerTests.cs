using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Byte.Blog.Rendering.Controllers;
using Byte.Blog.Rendering.Models;
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

            var pages = this.GetTestPages(pagesCount);

            using (var navigationController = new NavigationController(store))
            {
                using (var session = store.OpenSession())
                {
                    foreach (var page in pages)
                    {
                        session.Store(page);
                    }
                    session.SaveChanges();

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

            var pages = this.GetTestPages(pagesCount);

            using (var navigationController = new NavigationController(store))
            {
                using (var session = store.OpenSession())
                {
                    foreach (var page in pages)
                    {
                        session.Store(page);
                    }
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(navigationController, session);
                    
                    var actionResult = navigationController.Menu();
                    var pageViewModels = ControllerTestHelper.GetModelInActionResult<IEnumerable<PageViewModel>>(actionResult);

                    foreach (var page in pages)
                    {
                        Assert.True(pageViewModels.Any(vm => vm.Id == page.Id));
                    }
                }
            }

            Mapper.Reset();
        }

        private IEnumerable<Page> GetTestPages(int number)
        {
            return Enumerable.Range(0, number)
                .Select(n =>
                    new Page
                    {
                        Id = "pages/" + n,
                        Title = "page " + n
                    });
        }
    }
}
