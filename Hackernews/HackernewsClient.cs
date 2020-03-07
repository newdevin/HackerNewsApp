using Hackernews.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews
{
    public class HackernewsClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IUriProvider uriProvider;

        public HackernewsClient(IHttpClientFactory httpClientFactory, IUriProvider uriProvider)
        {
            this.httpClientFactory = httpClientFactory;
            this.uriProvider = uriProvider;
        }

        public async Task<IEnumerable<Post>> GetTopPosts(int top)
        {
            var uri = uriProvider.GetTopPostsUri();
            var result = await GetResponse(uri,"topPosts");

            var topPosts = await TopPosts(result, top);
            UpdateRank(topPosts);
            return topPosts;
        }

        private void UpdateRank(IEnumerable<Post> topPosts)
        {

            for (int i = 0; i < topPosts.Count(); i++)
            {
                var post = topPosts.ElementAt(i);
                post.Rank = i + 1;
            }
        }

        private async Task<string> GetResponse(Uri uri, string clientName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = httpClientFactory.CreateClient(clientName);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result= await response.Content.ReadAsStringAsync();
                return result;
            }
            else
                return $"Error : Status code: {response.StatusCode}";
        }

        private async Task<IEnumerable<Post>> TopPosts(string result, int top)
        {
            var allPosts = JsonConvert.DeserializeObject<int[]>(result);
            var topPosts = allPosts.Take(top);

            var tasks = topPosts.AsParallel().AsOrdered()
                .Select(async (int i) =>
                {
                    return await GetPost(i);
                }).ToList();

            await Task.WhenAll(tasks);

            return tasks.Select(t => t.Result);
        }

        private async Task<Post> GetPost(int i)
        {
            var uri = uriProvider.GetItemUri(i);
            var result = await GetResponse(uri,"items");
            var hackernewsPost = JsonConvert.DeserializeObject<HackernewsPost>(result);
            return ToPost(hackernewsPost);
        }

        private Post ToPost(HackernewsPost hackernewsPost)
        {
            return new Post
            {
                Author = hackernewsPost.By,
                Comments = hackernewsPost.Kids.Count(),
                Points = hackernewsPost.Score,
                Title = hackernewsPost.Title,
                Uri = hackernewsPost.Url
            };
        }
    }
}
