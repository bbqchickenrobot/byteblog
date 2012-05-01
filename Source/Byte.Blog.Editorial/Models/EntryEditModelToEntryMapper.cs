using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework;
using Raven.Client;

namespace Byte.Blog.Editorial.Models
{
    public class EntryEditModelToEntryMapper
    {
        private readonly IDocumentSession session;

        public EntryEditModelToEntryMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public Entry Map(EntryEditModel editModel)
        {
            var entry = Mapper.Map<Entry>(editModel);

            entry.Slug = (new SlugMaker()).CreateSlug(entry.Title);

            return entry;
        }
    }
}
