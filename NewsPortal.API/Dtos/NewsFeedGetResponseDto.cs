using NewsPortal.API.Entites;

namespace NewsPortal.API.Dtos
{
    public class NewsFeedGetResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }

        public static explicit operator NewsFeedGetResponseDto(NewsFeed source) 
            => source is null ? new NewsFeedGetResponseDto() :
            new NewsFeedGetResponseDto() 
            {
                Id = source.Id,
                Title = source.Title,
                Summary = source.Summary,
                Description = source.Description,
                CategoryId = source.CategoryId,
                CategoryName = source.Category?.Name,
                CreatedDate = source.CreatedDate
            };

        public static IEnumerable<NewsFeedGetResponseDto> CreateList(IEnumerable<NewsFeed> source)
        {
            var target = new List<NewsFeedGetResponseDto>();
            foreach (var item in source)
            {
                target.Add((NewsFeedGetResponseDto)item);
            }
            return target;
        }

    }
}
