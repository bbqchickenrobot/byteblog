using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Controllers;
using Byte.Blog.Framework.UnitTests;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Controllers
{
    public class TagsControllerTests
    {
        [Fact]
        public void Search_returns_only_distinct_results()
        {
            var store = new TestableStore();

            var duplicatedTag = "alpha";

            var testTags = new[] { duplicatedTag, duplicatedTag, "beta" }.ToList();

            using (var tagsController = new TagsController(store))
            {
                using (var session = store.OpenSession())
                {
                    var entry = new Entry { Tags = new Collection<string>(testTags) };
                    session.Store(entry);
                    session.SaveChanges();

                    RavenControllerTestHelper.SetSessionOnController(tagsController, session);

                    var actionResult = tagsController.Search("alpha");
                    var tags = GetTagsFromResult(actionResult);

                    Assert.Equal(1, tags.Count());
                    Assert.Equal(duplicatedTag, tags.First());
                }
            }
        }

        [Fact]
        public void Search_results_include_searched_term()
        {
            var store = new TestableStore();

            using (var tagsController = new TagsController(store))
            {
                using (var session = store.OpenSession())
                {
                    RavenControllerTestHelper.SetSessionOnController(tagsController, session);

                    var actionResult = tagsController.Search("blah");
                    var tags = GetTagsFromResult(actionResult);

                    Assert.Equal(1, tags.Count());
                    Assert.Equal("blah", tags.First());
                }
            }
        }

        private static IEnumerable<string> GetTagsFromResult(ActionResult actionResult)
        {
            var jsonResult = actionResult as JsonResult;
            if (jsonResult == null)
            {
                return Enumerable.Empty<string>();
            }

            return jsonResult.Data as IEnumerable<string>;
        }
    }
}
