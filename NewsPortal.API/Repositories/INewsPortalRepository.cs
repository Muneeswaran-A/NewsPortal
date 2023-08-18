using NewsPortal.API.Entites;
using NewsPortal.API.Models;

namespace NewsPortal.API.Repositories
{
    public interface INewsPortalRepository
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<NewsFeed> GetNewsFeeds();
        IEnumerable<NewsFeed> GetNewsFeedsWithFilter(NewsFeedFilter filter, out int totalRecords);
        NewsFeed GetNewsFeedById(int id);
        void InsertNewsFeed(NewsFeed newsFeed);
        void UpdateNewsFeed(NewsFeed newsFeed);
        void DeleteNewsFeed(int id);
        void SaveChanges();
    }
}
