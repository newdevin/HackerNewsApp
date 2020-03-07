using Hackernews.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mono.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Hackernews
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceProvider services = InitializeServices(args);
                CommandLineParser parser = services.GetService<CommandLineParser>();
                var posts = parser.GetPosts();
                if (!IsValid(posts))
                {
                    return;
                }

                var httpClientFactory = services.GetService<IHttpClientFactory>();
                var uriProvider = services.GetService<IUriProvider>();

                var client = new HackernewsClient(httpClientFactory, uriProvider);

                var topPosts = client.GetTopPosts(posts.Value).Result;
                PrettyPrint(topPosts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        private static void PrettyPrint(IEnumerable<Post> topPosts)
        {
            var jsonString = JsonConvert.SerializeObject(topPosts);
            string formatted = JValue.Parse(jsonString).ToString(Formatting.Indented);
            Console.WriteLine(formatted);

        }

        private static ServiceProvider InitializeServices(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");


            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection();
            serviceProvider.AddHttpClient();
            serviceProvider.AddTransient<IUriProvider>(services =>
            {
                return new HackernewUriProvider(configuration);
            });
            serviceProvider.AddTransient<CommandLineParser>(services => new CommandLineParser(args));

            var services = serviceProvider.BuildServiceProvider();
            return services;
        }

        private static bool IsValid(int? posts)
        {
            if (!posts.HasValue)
            {
                Console.WriteLine("ERROR : posts is a required argument. The valid value for posts is between 1 and 100");
                return false;
            }
            if (posts < 1 || posts > 100)
            {
                Console.WriteLine("ERROR : The valid value for posts is between 1 and 100");
                return false;
            }
            return true;
        }
    }
}
