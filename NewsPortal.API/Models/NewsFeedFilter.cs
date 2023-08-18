namespace NewsPortal.API.Models
{
    public class NewsFeedFilter: PagingFilter
    {
        public string SearchText { get; set; } = "";    
    }
}
