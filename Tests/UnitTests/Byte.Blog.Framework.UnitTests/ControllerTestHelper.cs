using System.Web.Mvc;

namespace Byte.Blog.Framework.UnitTests
{
    public static class ControllerTestHelper
    {
        public static T GetModelInActionResult<T>(ActionResult actionResult) where T : class
        {
            var viewResultBase = actionResult as ViewResultBase;
            if (viewResultBase == null)
            {
                return null;
            }

            return viewResultBase.ViewData.Model as T;
        }
    }
}
