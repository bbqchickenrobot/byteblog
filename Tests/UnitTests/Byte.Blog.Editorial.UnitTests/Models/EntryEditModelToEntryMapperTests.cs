using System;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework.UnitTests;
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
                var entry = new Entry();

                var entryEditModelToEntryMapper = new EntryEditModelToEntryMapper(session);
                entryEditModelToEntryMapper.Map(entry, entryEditModel);

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

                var entryEditModelToEntryMapper = new EntryEditModelToEntryMapper(session);
                entryEditModelToEntryMapper.Map(entry, entryEditModel);

                Assert.Equal(entry.Slug, entry.Slug);
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
                var entry = new Entry();

                var entryEditModelToEntryMapper = new EntryEditModelToEntryMapper(session);
                entryEditModelToEntryMapper.Map(entry, entryEditModel);

                Assert.NotEqual(entryEditModel.LastModifiedAtUtc, entry.LastModifiedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Published_date_maps()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var testableStore = new TestableStore();

            var pubDate = DateTimeOffset.MinValue;

            using (var session = testableStore.OpenSession())
            {
                var entryEditModel = new EntryEditModel
                {
                    PublishedAtUtc = pubDate
                };

                var entry = new Entry();

                var entryEditModelToEntryMapper = new EntryEditModelToEntryMapper(session);
                entryEditModelToEntryMapper.Map(entry, entryEditModel);

                Assert.Equal(pubDate, entry.PublishedAtUtc);
            }

            Mapper.Reset();
        }
    }
}
