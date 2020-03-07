using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.Model
{
    public class Post
    {
        public string Title { get; set; }
        public Uri Uri { get; set; }
        public string Author { get; set; }
        public int Points { get; set; }
        public int Comments { get; set; }
        public int Rank { get; set; }
    }
}
