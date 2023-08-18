using Microsoft.AspNetCore.Mvc;
using NewsPortal.API.Dtos;
using NewsPortal.API.Repositories;
using System.Net;

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly INewsPortalRepository newsPortalRepository;

        public CategoryController(INewsPortalRepository newsPortalRepository)
        {
            this.newsPortalRepository = newsPortalRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryGetResponseDto>> Get()
        {
            try
            {
                var categories = newsPortalRepository.GetCategories();
                if (categories == null || !categories.Any())
                    return NotFound(new ProblemDetails() { Detail = "No data exists.", Status = (int)HttpStatusCode.NoContent, Title = "Not found" });
                return Ok(CategoryGetResponseDto.CreateList(categories));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails() { Detail = ex.Message, Status = (int)HttpStatusCode.BadRequest, Title = "Exception" });
            }
        }
    }
}
