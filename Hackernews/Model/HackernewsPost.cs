using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.Model
{
    public class HackernewsPost
    {
        public string By { get; set; }
        public Uri Url { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public List<int> Kids { get; set; } = new List<int>();
        public int Score { get; set; }
    }
}
