using Microsoft.Extensions.Configuration;
using System;

namespace Hackernews
{
    public class HackernewUriProvider : IUriProvider
    {
        private readonly IConfiguration configuration;

        public HackernewUriProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Uri GetItemUri(int id)
        {
            var value = string.Format(GetSection("hackernewsItemUri"), id);
            return new Uri(value);
        }

        public Uri GetTopPostsUri()
        {
            var value = GetSection("hackernewsTopStoriesUri");
            return new Uri(value);

        }

        private string GetSection(string key)
        {
            return configuration.GetValue<string>(key);
        }
    }
}
