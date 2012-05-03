using AutoMapper;
using Byte.Blog.Content;
using Byte.Blog.Rendering.Models;
using Xunit;

namespace Byte.Blog.Rendering.UnitTests.Models
{
    public class AutoMapperConfigTests
    {
        [Fact]
        public void Can_map_Entry_to_EntryViewModel_without_throwing()
        {
            Mapper.Reset();

            AutoMapperConfig.RegisterMappings();

            var entry = new Entry();

            Assert.DoesNotThrow(() => Mapper.Map<EntryViewModel>(entry));

            Mapper.Reset();
        }

        [Fact]
        public void Can_map_Page_to_PageViewModel_without_throwing()
        {
            Mapper.Reset();

            AutoMapperConfig.RegisterMappings();

            var page = new Page();

            Assert.DoesNotThrow(() => Mapper.Map<PageViewModel>(page));

            Mapper.Reset();
        }
    }
}
