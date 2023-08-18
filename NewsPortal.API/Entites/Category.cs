namespace NewsPortal.API.Entites
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<NewsFeed> NewsFeeds { get; set; }
    }
}
