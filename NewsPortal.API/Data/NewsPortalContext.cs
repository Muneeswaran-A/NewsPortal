using Microsoft.EntityFrameworkCore;
using NewsPortal.API.Entites;

namespace NewsPortal.API.Data
{
    public class NewsPortalContext : DbContext
    {
        public NewsPortalContext(DbContextOptions options) : base(options) { }

        public DbSet<NewsFeed> NewsFeeds { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
