using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.Model
{
    public class Post
    {
        public Post(string title, Uri uri, string author, int points, int comments)
        {
            ThrowIfValidationFails(title, author, points, comments);

            Title = title;
            Uri = uri;
            Author = author;
            Points = points;
            Comments = comments;
        }

        private static void ThrowIfValidationFails(string title, string author, int points, int comments)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException($"{nameof(title)} can't be empty or null");
            if (string.IsNullOrEmpty(author))
                throw new ArgumentException($"{nameof(author)} can't be empty or null");
            if (title.Length > 256)
                throw new ArgumentException($"{nameof(title)} can't be more than 256 characters long");
            if (author.Length > 256)
                throw new ArgumentException($"{nameof(author)} can't be more than 256 characters long");
            if (points < 0)
                throw new ArgumentException($"{nameof(points)} must be a positive number");
            if (comments < 0)
                throw new ArgumentException($"{nameof(comments)} must be a positive number");
        }

        public int Rank { get; set; }
        public string Title { get; }
        public Uri Uri { get; }
        public string Author { get; }
        public int Points { get; }
        public int Comments { get; }
    }
}
