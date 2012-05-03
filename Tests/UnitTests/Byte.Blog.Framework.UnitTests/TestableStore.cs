using Raven.Client.Embedded;

namespace Byte.Blog.Framework.UnitTests
{
    public class TestableStore : EmbeddableDocumentStore
    {
        public TestableStore()
        {
            this.RunInMemory = true;
            this.Initialize();
        }

        public bool Contains(string id)
        {
            using (var session = this.OpenSession())
            {
                var foo = session.Load<dynamic>(id);
                return foo != null;
            }
        }
    }
}
