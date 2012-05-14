using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Byte.Blog.Framework.UnitTests.Web;
using Byte.Blog.Rendering.Models;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests.Models
{
    public class PageToPageViewModelMapperTests
    {
        [Fact]
        public void Page_title_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string title = "foo";

            var page = new Page
            {
                Title = title
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var urlHelper = TestableUrlHelper.Create();

                var pageToPageViewModelMapper = new PageToPageViewModelMapper(session, urlHelper);
                var pageViewModel = pageToPageViewModelMapper.Map(page);

                Assert.Equal(title, pageViewModel.Title);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Page_slug_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string slug = "foo-bar-baz";

            var page = new Page
            {
                Slug = slug
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var urlHelper = TestableUrlHelper.Create();

                var pageToPageViewModelMapper = new PageToPageViewModelMapper(session, urlHelper);
                var pageViewModel = pageToPageViewModelMapper.Map(page);

                Assert.Equal(slug, pageViewModel.Slug);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Page_html_color_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string color = "#ff0000";

            var page = new Page
            {
                HtmlColor = color
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var urlHelper = TestableUrlHelper.Create();

                var pageToPageViewModelMapper = new PageToPageViewModelMapper(session, urlHelper);
                var pageViewModel = pageToPageViewModelMapper.Map(page);

                Assert.Equal(color, pageViewModel.HtmlColor);
            }

            Mapper.Reset();
        }
    }
}