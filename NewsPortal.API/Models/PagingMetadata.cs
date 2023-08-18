namespace NewsPortal.API.Models
{
    public class PagingMetadata
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPrevious => PageNo > 1;
        public bool HasNext => PageNo < TotalPages;
    }
}
