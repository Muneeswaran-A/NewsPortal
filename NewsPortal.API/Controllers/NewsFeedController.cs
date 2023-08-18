using Microsoft.AspNetCore.Mvc;
using NewsPortal.API.Dtos;
using NewsPortal.API.Entites;
using NewsPortal.API.Models;
using NewsPortal.API.Repositories;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsPortalRepository newsPortalRepository;

        public NewsFeedController(INewsPortalRepository newsPortalRepository)
        {
            this.newsPortalRepository = newsPortalRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NewsFeedGetResponseDto>> Get()
        {
            try
            {
                var newsFeeds = newsPortalRepository.GetNewsFeeds();
                if (newsFeeds == null || !newsFeeds.Any())
                    return NotFound(new ProblemDetails() { Detail = "No data exists.", Status = (int)HttpStatusCode.NoContent, Title = "Not found" });

                return Ok(NewsFeedGetResponseDto.CreateList(newsFeeds));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }

        [HttpGet]
        public ActionResult<PagedList<NewsFeedGetResponseDto>> GetWithFilter([FromQuery] NewsFeedFilter newsFeedFilter)
        {
            try
            {
                var newsFeeds = newsPortalRepository.GetNewsFeedsWithFilter(newsFeedFilter, out int totalRecords);
                if (newsFeeds == null || !newsFeeds.Any())
                    return NotFound(new ProblemDetails() { Detail = "No data exists.", Status = (int)HttpStatusCode.NoContent, Title = "Not found" });

                var items = NewsFeedGetResponseDto.CreateList(newsFeeds);
                return Ok(PagedList<NewsFeedGetResponseDto>.Create(items, newsFeedFilter.PageNo, newsFeedFilter.PageSize, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NewsFeedGetResponseDto> Get(int id)
        {
            try
            {
                var newsFeed = newsPortalRepository.GetNewsFeedById(id);
                if (newsFeed == null)
                    return NotFound(new ProblemDetails() { Detail = "No data exists.", Status = (int)HttpStatusCode.NotFound, Title = "Not found" });

                return (NewsFeedGetResponseDto)newsFeed;
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] NewsFeedAddRequestDto value)
        {
            try
            {
                NewsFeed newsFeed = value;
                newsPortalRepository.InsertNewsFeed(newsFeed);
                newsPortalRepository.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] NewsFeedUpdateRequestDto value)
        {
            try
            {
                NewsFeed newsFeed = value;
                newsFeed.Id = id;
                newsPortalRepository.UpdateNewsFeed(newsFeed);
                newsPortalRepository.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                newsPortalRepository.DeleteNewsFeed(id);
                newsPortalRepository.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }
    }
}
