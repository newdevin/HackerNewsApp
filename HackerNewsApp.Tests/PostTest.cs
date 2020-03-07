using Hackernews.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HackerNewsApp.Tests
{
    public class PostTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowIfTitleIsEmptyOrNull(string title)
        {
            Assert.Throws<ArgumentException>(() => new Post(title, new Uri("http://someuri.com"), "Luke", 10, 5));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowIfAuthorIsEmptyOrNull(string author)
        {
            Assert.Throws<ArgumentException>(() => new Post("Just di it", new Uri("http://someuri.com"), author, 10, 5));
        }

        [Fact]
        public void ShouldThrowIfTitleIsGraterThan256Chars()
        {
            var title = Enumerable.Range(1, 257).Select(_ => "A").Aggregate((a, b) => $"{a}{b}");

            Assert.Throws<ArgumentException>(() => new Post(title, new Uri("http://someuri.com"), "Luke", 10, 5));
        }
        [Fact]
        public void ShouldThrowIfAuthorIsGraterThan256Chars()
        {
            var author= Enumerable.Range(1, 257).Select(_ => "A").Aggregate((a, b) => $"{a}{b}");

            Assert.Throws<ArgumentException>(() => new Post("Just do it", new Uri("http://someuri.com"), author, 10, 5));
        }

        [Fact]
        public void ShouldThrowIfPointsAreLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => new Post("Just do it", new Uri("http://someuri.com"), "Luke", -1, 5));
        }

        [Fact]
        public void ShouldThrowIfCommentsAreLessThanZero()
        {
            Assert.Throws<ArgumentException>(() => new Post("Just do it", new Uri("http://someuri.com"), "Luke", 2, -1));
        }
    }
}
