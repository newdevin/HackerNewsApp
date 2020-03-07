using Hackernews.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Hackernews.Tests
{
    public class HackernewsClientTest
    {
        readonly IUriProvider uriProvider;
        readonly IHttpClientFactory httpClientFactory;
        readonly IConfiguration configuration;
        public HackernewsClientTest()
        {
            uriProvider = Substitute.For<IUriProvider>();
            httpClientFactory = Substitute.For<IHttpClientFactory>();
            configuration = Substitute.For<IConfiguration>();
        }

        [Fact]
        public void ShouldReturnPostsFromJson()
        {
            var posts = GetPosts();
            var ids = new List<int> { 1, 2, 3 };
            var fakeHttpMessageHandler = new FakeItemsHttpMessageHandler(posts.ToList());

            var fakeHttpMessageHandler2 = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            });

            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var fakeHttpClient2 = new HttpClient(fakeHttpMessageHandler2);
            httpClientFactory.CreateClient(Arg.Is("items")).Returns(fakeHttpClient);
            httpClientFactory.CreateClient(Arg.Is("topPosts")).Returns(fakeHttpClient2);

            uriProvider.GetTopPostsUri().Returns(new Uri("http://someuri.com"));
            uriProvider.GetItemUri(Arg.Any<int>()).Returns((i) => new Uri("http://someuri.com"));


            var hackernewsClient = new HackernewsClient(httpClientFactory, uriProvider, new MApper());
            var actualPosts = hackernewsClient.GetTopPosts(posts.Count()).Result;

            Assert.Equal(posts.Count(), actualPosts.Count());

            foreach(var post in actualPosts)
            {
                var testPost = posts.First(p => p.By == post.Author);
                Assert.Equal(testPost.Title, post.Title);
                Assert.Equal(testPost.Url.ToString(), post.Uri.ToString());
                Assert.Equal(testPost.Kids.Count(), post.Comments);
                Assert.Equal(testPost.Score, post.Points);
            }

        }

        private IEnumerable<HackernewsPost> GetPosts()
        {
            return new List<HackernewsPost>
            {
                new HackernewsPost { By = "John", Id = 1, Kids = new List<int>{1,2,3}, Score = 10, Title = "Just do it", Url = new Uri("http://justdoit.com")},
                new HackernewsPost { By = "Luke", Id = 2, Kids = new List<int>{11,12,13}, Score = 20, Title = "Just did it", Url = new Uri("http://justdidit.com")},
                new HackernewsPost { By = "Nathan", Id = 3, Kids = new List<int>{21,22,23}, Score = 30, Title = "Just did it again", Url = new Uri("http://justdiditagain.com")}
            };
        }
    }
}
