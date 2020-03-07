using Mono.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews
{
    public class CommandLineParser
    {
        private readonly string[] arguments;

        public CommandLineParser(string[] arguments)
        {
            this.arguments = arguments;
        }
        public int? GetPosts()
        {
            int? posts = null;
            var options = new OptionSet()
                .Add("posts=", "REQUIRED: posts - The number of posts to be fetched (between 1 and 100)",
                (int opt) => posts = opt);

            try
            {
                options.Parse(arguments);
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
            }
            return posts;
        }
    }
}
