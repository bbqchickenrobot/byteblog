using System.Linq;
using Xunit;

namespace Byte.Blog.Framework.UnitTests
{
    public class SlugMakerTests
    {
        [Fact]
        public void Slug_generated_does_not_exceed_max_length()
        {
            int maxLength = 50;

            string input = string.Join("", Enumerable.Range(0, 51).Select(n => "a"));

            var slugMaker = new SlugMaker(maxLength);
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(maxLength, slug.Length);
        }

        [Fact]
        public void Slug_generated_is_all_lowercase()
        {
            string input = "Foo BaR BAZ";
            string expected = "foo-bar-baz";

            var slugMaker = new SlugMaker();
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(expected, slug);
        }

        [Fact]
        public void Only_alphanumeric_characters_exist_in_generated_slug()
        {
            string input = "Foo BaR BAZ%^@ () ";
            string expected = "foo-bar-baz";

            var slugMaker = new SlugMaker();
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(expected, slug);
        }

        [Fact]
        public void Multiple_whitespace_do_not_result_in_double_dashes()
        {
            string input = "foo  bar       baz";
            string expected = "foo-bar-baz";

            var slugMaker = new SlugMaker();
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(expected, slug);
        }

        [Fact]
        public void Slug_has_no_leading_or_trailing_whitespace()
        {
            string input = "       Foo Bar Baz      ";
            string expected = "foo-bar-baz";

            var slugMaker = new SlugMaker();
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(expected, slug);
        }

        [Fact]
        public void Spaces_are_converted_to_dashes()
        {
            string input = "foo bar baz";
            string expected = "foo-bar-baz";

            var slugMaker = new SlugMaker();
            string slug = slugMaker.CreateSlug(input);

            Assert.Equal(expected, slug);
        }
    }
}
