using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Byte.Blog.Framework.UnitTests.Web
{
    public static class TestableUrlHelper
    {
        public static UrlHelper Create()
        {
            var context = new Mock<HttpContextBase>();
            var requestContext = new RequestContext(context.Object, new RouteData());
            return new UrlHelper(requestContext);
        }
    }
}
