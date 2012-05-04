using System.Linq;
using Byte.Blog.Content;
using Raven.Client;

namespace Byte.Blog.Rendering
{
    public class EntryFactory
    {
        private readonly IDocumentSession session;

        public EntryFactory(IDocumentSession session)
        {
            this.session = session;
        }

        public Entry CreateFromSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            var entry = this.session.Query<Entry>()
                .FirstOrDefault(e => e.Slug == slug && !e.Deleted && e.Published);

            return entry;
        }
    }
}
