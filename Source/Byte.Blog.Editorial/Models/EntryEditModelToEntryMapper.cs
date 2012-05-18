using System;
using System.Collections.ObjectModel;
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

        public void Map(Entry entry, EntryEditModel entryEditModel)
        {
            MapBasicProperties(entry, entryEditModel);
            SetSlug(entry);
            UpdateTimestamps(entry, entryEditModel);
        }

        private void MapBasicProperties(Entry entry, EntryEditModel editModel)
        {
            entry.Body = editModel.Body;
            entry.Deleted = editModel.Deleted;
            entry.PageId = editModel.PageId;
            entry.Published = editModel.Published;
            entry.Tags = new Collection<string>(editModel.Tags);
            entry.Title = editModel.Title;
        }

        private static void SetSlug(Entry entry)
        {
            if (string.IsNullOrEmpty(entry.Slug))
            {
                var slugMaker = new SlugMaker();
                entry.Slug = slugMaker.CreateSlug(entry.Title);
            }
        }

        private static void UpdateTimestamps(Entry entry, EntryEditModel entryEditModel)
        {
            entry.LastModifiedAtUtc = DateTimeOffset.UtcNow;
            entry.PublishedAtUtc = entryEditModel.PublishedAtUtc;
        }
    }
}
