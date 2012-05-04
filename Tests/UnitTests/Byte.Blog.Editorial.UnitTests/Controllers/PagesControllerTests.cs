using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework;
using Byte.Blog.Framework.UnitTests;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class PagesControllerTests
    {
        [Fact]
        public void Edit_pulls_page_from_database_and_creates_viewmodel()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            var page = new Page
            {
                Id = "pages/123",
                Title = "foo"
            };

            using (var pagesController = new PagesController(store, new SlugMaker()))
            {
                using (var session = store.OpenSession())
                {
                    session.Store(page);
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(pagesController, session);

                    string uniquePart = page.Id.Replace(Page.IdPrefix, "");

                    var actionResult = pagesController.Edit(uniquePart);
                    var pageEditModel = ControllerTestHelper.GetModelInActionResult<PageEditModel>(actionResult);

                    Assert.Equal(page.Title, pageEditModel.Title);
                }
            }

            Mapper.Reset();
        }

        [Fact]
        public void Save_persists_valid_page_to_database()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string id = "foo/123";

            var store = new TestableStore();

            using (var pagesController = new PagesController(store, new SlugMaker()))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(pagesController, session);
                    pagesController.Save(new PageEditModel() { Id = id, Title = "foo" });
                }
            }

            Assert.True(store.Contains(id));

            Mapper.Reset();
        }

        [Fact]
        public void Delete_sets_page_deleted()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var store = new TestableStore();

            var page = new Page
            {
                Id = "pages/123",
                Title = "foo"
            };

            using (var pagesController = new PagesController(store, new SlugMaker()))
            {
                using (var session = store.OpenSession())
                {
                    session.Store(page);
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(pagesController, session);

                    pagesController.Delete(page.Id);

                    var pageLoaded = session.Load<Page>(page.Id);
                    Assert.True(pageLoaded.Deleted);
                }
            }

            Mapper.Reset();
        }
    }
}
