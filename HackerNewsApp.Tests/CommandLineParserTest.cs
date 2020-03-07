using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Hackernews;

namespace Hackernews.Tests
{
    public class CommandLineParserTest
    {
        [Fact]
        public void ShouldReturnFalseIfPostsArgumentIsMissing()
        {
            var inputs = new string[0];
            var commandLineParser = new CommandLineParser(inputs);
            var posts = commandLineParser.GetPosts();
            Assert.False(posts.HasValue);
        }

        [Fact]
        public void ShouldReturnFalseWhenPostsArgumentIsNotSupplied()
        {
            var inputs = new string[] { "--posts"};
            var commandLineParser = new CommandLineParser(inputs);
            var posts = commandLineParser.GetPosts();
            Assert.False(posts.HasValue);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("20")]
        [InlineData("100")]
        public void ShouldReturnCorrectValueForPostsArgument(string s)
        {
            var inputs = new string[] { "--posts" , s };
            var commandLineParser = new CommandLineParser(inputs);
            var posts = commandLineParser.GetPosts();
            Assert.True(posts.HasValue);
            Assert.Equal(s, posts.Value.ToString());
        }
        
    }
}
