using Hackernews.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackernews
{
    public class Mapper : IMapper
    {
        public Post ToPost(HackernewsPost hackernewsPost)
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
