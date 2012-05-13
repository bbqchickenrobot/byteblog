using System;
using System.Collections.ObjectModel;
using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Framework.UnitTests;
using Byte.Blog.Rendering.Models;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests.Models
{
    public class EntryToEntryViewModelMapperTests
    {
        [Fact]
        public void Entry_title_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string title = "foo";

            var entry = new Entry
            {
                Title = title
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(title, entryViewModel.Title);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_deleted_flag_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var entry = new Entry
            {
                Deleted = true
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(true, entryViewModel.Deleted);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_id_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string id = "entries/123";

            var entry = new Entry
            {
                Id = id
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(id, entryViewModel.Id);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_LastModifiedAtUtc_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var lastModifiedAtUtc = DateTimeOffset.UtcNow;

            var entry = new Entry
            {
                LastModifiedAtUtc = lastModifiedAtUtc
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(lastModifiedAtUtc, entryViewModel.LastModifiedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_page_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string pageId = "pages/123";

            var entry = new Entry
            {
                PageId = pageId
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(pageId, entryViewModel.PageId);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_published_flag_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var entry = new Entry
            {
                Published = true
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(true, entryViewModel.Published);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_PublishedAtUtc_is_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var publishedAtUtc = DateTimeOffset.UtcNow;

            var entry = new Entry
            {
                PublishedAtUtc = publishedAtUtc
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(publishedAtUtc, entryViewModel.PublishedAtUtc);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_references_are_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var references = new Collection<string>(new[] { "images/123" });

            var entry = new Entry
            {
                References = references
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(references, entryViewModel.References);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_slug_are_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            string slug = "foo-bar-baz";

            var entry = new Entry
            {
                Slug = slug
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(slug, entryViewModel.Slug);
            }

            Mapper.Reset();
        }

        [Fact]
        public void Entry_tags_are_correctly_mapped()
        {
            Mapper.Reset();
            AutoMapperConfig.RegisterMappings();

            var tags = new Collection<string>(new[] { "iphone", "android" });

            var entry = new Entry
            {
                Tags = tags
            };

            var store = new TestableStore();

            using (var session = store.OpenSession())
            {
                var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(session);
                var entryViewModel = entryToEntryViewModelMapper.Map(entry);

                Assert.Equal(tags, entryViewModel.Tags);
            }

            Mapper.Reset();
        }
    }
}