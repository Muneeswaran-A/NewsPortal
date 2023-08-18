namespace NewsPortal.API.Models
{
    public class PagedList<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public PagingMetadata Metadata { get; set; }

        public PagedList()
        {
            Items = Enumerable.Empty<T>();
            Metadata = new PagingMetadata();
        }

        public PagedList(IEnumerable<T> items,  int pageNo, int pageSize, int totalRecords)
        {
            Metadata = new PagingMetadata()
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
            Items = items;
        }

        public static PagedList<T> Create(IEnumerable<T> items, int pageNo, int pageSize, int totalRecords)
        {
            return new PagedList<T>(items, pageNo, pageSize, totalRecords);
        }
    }
}
