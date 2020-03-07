using Hackernews.Model;

namespace Hackernews
{
    public interface IMapper
    {
        Post ToPost(HackernewsPost hackernewsPost);
    }
}
