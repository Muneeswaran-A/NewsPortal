using NewsPortal.API.Entites;

namespace NewsPortal.API.Dtos
{
    public class CategoryGetResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator CategoryGetResponseDto(Category source)
            => source is null ? new CategoryGetResponseDto():
            new CategoryGetResponseDto()
            {
                Id = source.Id,
                Name = source.Name
            };

        public static IEnumerable<CategoryGetResponseDto> CreateList(IEnumerable<Category> source)
        {
            var target = new List<CategoryGetResponseDto>();
            foreach (var item in source)
            {
                target.Add((CategoryGetResponseDto)item);
            }
            return target;
        }
    }
}
