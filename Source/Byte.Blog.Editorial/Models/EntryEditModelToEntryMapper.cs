using System;
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
            var newEntry = Mapper.Map<Entry>(editModel);
            var existingEntry = this.GetExistingEntry(newEntry.Id);

            SetSlug(existingEntry, newEntry);

            this.UpdateTimestamps(existingEntry, newEntry);

            return newEntry;
        }

        private Entry GetExistingEntry(string id)
        {
            if (string.IsNullOrEmpty(id) || id == Entry.IdPrefix)
            {
                return null;
            }

            return this.session.Load<Entry>(id);
        }

        private static void SetSlug(Entry existingEntry, Entry newEntry)
        {
            if (existingEntry == null)
            {
                var slugMaker = new SlugMaker();
                newEntry.Slug = slugMaker.CreateSlug(newEntry.Title);
            }
            else
            {
                newEntry.Slug = existingEntry.Slug;
            }
        }

        private void UpdateTimestamps(Entry existingEntry, Entry newEntry)
        {
            UpdateLastModifiedAtUtc(newEntry);
            UpdatePublishedAtUtc(existingEntry, newEntry);
        }

        private static void UpdateLastModifiedAtUtc(Entry entry)
        {
            entry.LastModifiedAtUtc = DateTimeOffset.UtcNow;
        }

        private static void UpdatePublishedAtUtc(Entry existingEntry, Entry newEntry)
        {
            if (IsBecomingPublished(existingEntry, newEntry))
            {
                newEntry.PublishedAtUtc = DateTimeOffset.UtcNow;
            }
            else if (existingEntry != null)
            {
                newEntry.PublishedAtUtc = existingEntry.PublishedAtUtc;
            }
        }

        private static bool IsBecomingPublished(Entry existingEntry, Entry newEntry)
        {
            if (newEntry.Published && (existingEntry == null || !existingEntry.Published))
            {
                return true;
            }

            return false;
        }
    }
}
