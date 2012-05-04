using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhino.Mocks;

namespace Byte.Blog.Framework.UnitTests
{
    public static class HtmlHelperTestHelpers
    {
        public static HtmlHelper<T> CreateMockHtmlHelper<T>(ViewDataDictionary viewData) where T : class
        {
            var mocks = new MockRepository();

            var controllerContext = mocks.DynamicMock<ControllerContext>(
                mocks.DynamicMock<HttpContextBase>(),
                new RouteData(),
                mocks.DynamicMock<ControllerBase>());

            var mockViewContext = mocks.DynamicMock<ViewContext>(
                controllerContext,
                mocks.DynamicMock<IView>(),
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = mocks.DynamicMock<IViewDataContainer>();

            mockViewDataContainer.Expect(v => v.ViewData).Return(viewData);

            return new HtmlHelper<T>(mockViewContext, mockViewDataContainer);
        }
    }
}
