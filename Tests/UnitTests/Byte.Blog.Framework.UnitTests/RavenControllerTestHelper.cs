using System.Reflection;
using Byte.Blog.Framework.Data;
using Raven.Client;

namespace Byte.Blog.Framework.UnitTests
{
    public static class RavenControllerTestHelper
    {
        public static void SetSessionOnController(
            RavenController controller,
            IDocumentSession session)
        {
            typeof(RavenController)
                .GetProperty("session", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(controller, session, null);
        }
    }
}
