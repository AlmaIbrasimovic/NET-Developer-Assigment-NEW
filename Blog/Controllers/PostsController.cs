using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.DTO;
using Blog.Services;

namespace Blog.Controllers
{
    [Produces("application/json")]
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        // GET methods
        [HttpGet]
        public PostsResponseMultiple GetPosts(string tag = "all")
        {
            PostsResponseMultiple postRes = new PostsResponseMultiple();
            List<Post> result = new List<Post>();
            var tagList = _context.Posts.Include(x => x.tagList);

            switch (tag)
            {
                case "all":
                    result = tagList.ToList().OrderByDescending(c => (DateTime)c.createdAt).ToList();
                    break;
                default:
                    result = tagList.Where(t => t.tagList.Select(c => c.name).Contains(tag))
                             .OrderByDescending(p => (DateTime) p.createdAt).ToList();
                    break;
            }
            postRes.blogPosts = result;
            return postRes;
        }


        [HttpGet("{slug}")]
        public async Task<IActionResult> GetPost([FromRoute] String slug)
        {

            // If post with entered slug doesn't exist in DB
            var postSlug = _context.Posts.Find(slug);
            if (postSlug == null)
            {
                return BadRequest("Post with selected slug doesn't exist!");
            }

            // Including tagList in response
            PostResponseSingle postRes = new PostResponseSingle();
            var post = _context.Posts
                .Include(i => i.tagList)
                .First(x => x.slug == slug);
            postRes.blogPost = post;
            return Ok(postRes);
        }

       
        // PUT
        [HttpPut("{slug}")]
        public async Task<IActionResult> UpdatePost([FromRoute] String slug, [FromBody] Post post)
        {
            PostServicecs postServices = new PostServicecs(_context);

            if (slug != post.slug)
            {
                 return BadRequest("Post with selected slug doesn't exist!");
            }

            // Updating post if the title changes
            var oldPost = _context.Posts
                 .Include(i => i.tagList)
                 .First(x => x.slug == slug);

            if (oldPost.title != post.title && post.title != null)
            {
                 Post newPost = postServices.updatePostWithTitle(slug, post);
                 return Ok(newPost);
            }

            // Updating post if the title didn't change
            var entityPost = postServices.updatePost(slug, post);
            _context.Update(entityPost);
            await _context.SaveChangesAsync();
            return Ok(entityPost);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] PostDTO post)
        {
            Post newPost = new Post();
            PostServicecs postServices = new PostServicecs(_context);

            newPost = postServices.copyPostDTO(post);
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { slug = post.slug }, newPost);
        }

        // DELETE
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeletePost([FromRoute] String slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(slug);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(string id)
        {
            return _context.Posts.Any(e => e.slug == id);
        }
    }
}