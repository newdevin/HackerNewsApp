using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Hackernews
{
    public interface IUriProvider
    {
        Uri GetTopPostsUri();
        Uri GetItemUri(int id);
    }
}
