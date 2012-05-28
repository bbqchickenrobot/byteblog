using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Byte.Blog.Framework.UnitTests
{
    public class TestableStore : EmbeddableDocumentStore
    {
        public TestableStore(DocumentConvention conventions = null)
        {
            this.RunInMemory = true;

            if (conventions != null)
            {
                this.Conventions = conventions;
            }

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
