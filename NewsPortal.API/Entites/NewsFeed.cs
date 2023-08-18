namespace NewsPortal.API.Entites
{
    public class NewsFeed : EntityBase
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
