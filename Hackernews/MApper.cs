using Hackernews.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackernews
{
    public class MApper : IMapper
    {
        public Post ToPost(HackernewsPost hackernewsPost)
        {
            return new Post(hackernewsPost.Title, hackernewsPost.Url, hackernewsPost.By, hackernewsPost.Score, hackernewsPost.Kids.Count());

        }
    }
}
