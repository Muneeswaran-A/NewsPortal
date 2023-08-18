using NewsPortal.API.Entites;

namespace NewsPortal.API.Dtos
{
    public class NewsFeedAddRequestDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public static implicit operator NewsFeed(NewsFeedAddRequestDto source)
            => source is null ? new NewsFeed() :
            new NewsFeed()
            {
                Title = source.Title,
                Summary = source.Summary,
                Description = source.Description,
                CategoryId = source.CategoryId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
    }
}
