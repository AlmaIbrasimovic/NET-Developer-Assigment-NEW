using System.Linq;
using Blog.Classes;
using Microsoft.AspNetCore.Mvc;
using Blog.Data;

namespace Blog.Controllers
{
    [Route("api/tags")]
    [Produces("application/json")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly BlogContext _context;

        public TagsController(BlogContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public TagResponseMultiple GetTags()
        {
           TagResponseMultiple tagsResponse = new TagResponseMultiple();
           tagsResponse.tags = _context.Tags.ToList();
           return tagsResponse;
        }
    }
}