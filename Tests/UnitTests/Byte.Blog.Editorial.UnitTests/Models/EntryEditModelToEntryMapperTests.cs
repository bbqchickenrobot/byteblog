using System;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.UnitTests;
using Raven.Client;
using Xunit;

namespace Byte.Blog.Editorial.UnitTests.Models
{
    public class EntryEditModelToEntryMapperTests
    {
        [Fact]
        public void New_database_entry_has_slug_set_based_on_title()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entryEditModel = new EntryEditModel();
            entryEditModel.Title = "Foo Title";

            string expectedSlug = "foo-title";

            using (var session = testableStore.OpenSession())
            {
                var entry = GetMappedEntryFor(session, entryEditModel);

                Assert.Equal(expectedSlug, entry.Slug);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Existing_database_entry_slug_remains_unchanged()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entry = new Entry
            {
                Slug = "foo-title"
            };

            using (var session = testableStore.OpenSession())
            {
                session.Store(entry);
                session.SaveChanges();

                var entryEditModel = new EntryEditModel
                {
                    Id = entry.Id,
                    Title = "Bar Title"
                };

                var entryMapped = GetMappedEntryFor(session, entryEditModel);

                Assert.Equal(entry.Slug, entryMapped.Slug);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Last_modified_date_updates()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entryEditModel = new EntryEditModel
            {
                LastModifiedAtUtc = DateTimeOffset.MinValue
            };

            using (var session = testableStore.OpenSession())
            {
                var entry = GetMappedEntryFor(session, entryEditModel);

                Assert.NotEqual(entryEditModel.LastModifiedAtUtc, entry.LastModifiedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Existing_entry_already_published_leaves_published_date_unchanged()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entry = new Entry
            {
                PublishedAtUtc = DateTimeOffset.UtcNow.AddDays(-1)
            };

            using (var session = testableStore.OpenSession())
            {
                session.Store(entry);
                session.SaveChanges();

                var entryEditModel = new EntryEditModel
                {
                    Id = entry.Id,
                    PublishedAtUtc = DateTimeOffset.UtcNow
                };

                var entryMapped = GetMappedEntryFor(session, entryEditModel);

                Assert.Equal(entry.PublishedAtUtc, entryMapped.PublishedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Existing_entry_not_published_until_now_updates_published_date()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entry = new Entry();

            using (var session = testableStore.OpenSession())
            {
                session.Store(entry);
                session.SaveChanges();

                var entryEditModel = new EntryEditModel
                {
                    Id = entry.Id,
                    Published = true
                };

                var entryMapped = GetMappedEntryFor(session, entryEditModel);

                Assert.NotEqual(entry.PublishedAtUtc, entryMapped.PublishedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void New_unpublished_entry_leaves_published_date_unchanged()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var entry = new Entry();

            using (var session = testableStore.OpenSession())
            {
                session.Store(entry);
                session.SaveChanges();

                var entryEditModel = new EntryEditModel
                {
                    Id = entry.Id
                };

                var entryMapped = GetMappedEntryFor(session, entryEditModel);

                Assert.Equal(entry.PublishedAtUtc, entryMapped.PublishedAtUtc);
            }

            Mapper.Reset();
        }

        private static Entry GetMappedEntryFor(IDocumentSession session, EntryEditModel entryEditModel)
        {
            var entryEditModelToEntryMapper = new EntryEditModelToEntryMapper(session);

            return entryEditModelToEntryMapper.Map(entryEditModel);
        }
    }
}
