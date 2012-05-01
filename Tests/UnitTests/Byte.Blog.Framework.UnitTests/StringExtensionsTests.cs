using Xunit;

namespace Byte.Blog.Framework.UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Prefix_is_removed_from_string()
        {
            string input = "foobarbaz";
            string expected = "barbaz";

            string result = input.TrimPrefix("foo");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Non_existant_prefix_results_in_no_change()
        {
            string input = "barbaz";

            string result = input.TrimPrefix("foo");
            
            Assert.Equal(input, result);
        }

        [Fact]
        public void Suffix_is_removed_from_string()
        {
            string input = "foobarbaz";
            string expected = "foobar";

            string result = input.TrimSuffix("baz");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Non_existant_suffix_results_in_no_change()
        {
            string input = "foobar";

            string result = input.TrimSuffix("baz");

            Assert.Equal(input, result);
        }

        [Fact]
        public void Prefix_is_added_to_string()
        {
            string input = "barbaz";
            string expected = "foobarbaz";

            string result = input.EnsurePrefix("foo");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Prefix_already_present_results_in_no_change()
        {
            string input = "foobarbaz";

            string result = input.EnsurePrefix("foo");

            Assert.Equal(input, result);
        }

        [Fact]
        public void Suffix_is_added_to_string()
        {
            string input = "foobar";
            string expected = "foobarbaz";

            string result = input.EnsureSuffix("baz");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Suffix_already_present_results_in_no_change()
        {
            string input = "foobarbaz";

            string result = input.EnsureSuffix("baz");

            Assert.Equal(input, result);
        }
    }
}
