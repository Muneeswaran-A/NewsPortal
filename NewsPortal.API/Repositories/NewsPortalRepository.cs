using Microsoft.EntityFrameworkCore;
using NewsPortal.API.Data;
using NewsPortal.API.Entites;
using NewsPortal.API.Models;

namespace NewsPortal.API.Repositories
{
    public class NewsPortalRepository : INewsPortalRepository
    {
        private readonly NewsPortalContext newsPortalContext;

        public NewsPortalRepository(NewsPortalContext newsPortalContext)
        {
            this.newsPortalContext = newsPortalContext;
        }

        public IEnumerable<NewsFeed> GetNewsFeeds()
        {
            return newsPortalContext.NewsFeeds
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .AsNoTracking().ToList();
        }

        public IEnumerable<NewsFeed> GetNewsFeedsWithFilter(NewsFeedFilter filter, out int totalRecords)
        {
            totalRecords = newsPortalContext.NewsFeeds
                .Include(p => p.Category)
                .AsNoTracking()
                .Count(p => filter.SearchText != "" &&
                           EF.Functions.Like(p.Title, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Summary, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Description, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Category.Name, $"%{filter.SearchText}%")
                           );

            return newsPortalContext.NewsFeeds
                .Include(p => p.Category)
                .Where(p => filter.SearchText != "" &&
                           EF.Functions.Like(p.Title, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Summary, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Description, $"%{filter.SearchText}%") ||
                           EF.Functions.Like(p.Category.Name, $"%{filter.SearchText}%")
                           )
                .OrderByDescending(p => p.CreatedDate)
                .Skip((filter.PageNo - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking().ToList();
        }

        public NewsFeed GetNewsFeedById(int id)
        {
            return newsPortalContext.NewsFeeds.Include(p => p.Category).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public void InsertNewsFeed(NewsFeed newsFeed)
        {
            newsPortalContext.NewsFeeds.Add(newsFeed);
        }

        public void UpdateNewsFeed(NewsFeed updatedNewsFeed)
        {
            var newsFeed = GetNewsFeedById(updatedNewsFeed.Id);
            if (newsFeed is null)
                throw new Exception($"News feed with id '{updatedNewsFeed.Id}' does not exist.");

            updatedNewsFeed.CreatedDate = newsFeed.CreatedDate;
            newsPortalContext.Entry(updatedNewsFeed).State = EntityState.Modified;
        }
        
        public void DeleteNewsFeed(int id)
        {
            var newsFeed = newsPortalContext.NewsFeeds.Find(id);
            newsPortalContext.NewsFeeds.Remove(newsFeed);
        }

        public void SaveChanges()
        {
            newsPortalContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return newsPortalContext.Categories.AsNoTracking().OrderBy(p=>p.Name).ToList();
        }
    }
}
