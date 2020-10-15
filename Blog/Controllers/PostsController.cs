using System;
using System.Collections;
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
            var postSlug = _context.Posts.Where(a => a.slug.Contains(slug));
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
        public async Task<IActionResult> UpdatePost([FromRoute] String slug, [FromBody] PostResponseSingle postReq)
        {
            var post = postReq.blogPost;
            PostService postServices = new PostService(_context);

            if (slug != post.slug)
            {
                 return BadRequest("Post with selected slug doesn't exist!");
            }


            var oldPost = _context.Posts
                 .Include(i => i.tagList)
                 .First(x => x.slug == slug);


            var entityPost = postServices.updatePost(slug, post);
            _context.Update(entityPost);
            await _context.SaveChangesAsync();
            return Ok(entityPost);
            return Ok();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] PostRequestSingle postReq)
        {
            var post = postReq.blogPost;
            Post newPost = new Post();
            PostService postServices = new PostService(_context);

            newPost = postServices.copyPostDTO(post);
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { slug = post.slug }, newPost);
         
        }

        // DELETE
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeletePost([FromRoute] String slug)
        {
            PostService postServices = new PostService(_context);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _context.Posts.FirstOrDefaultAsync(e => e.slug == slug);
            
            if (result == null)
            {
                return NotFound();
            }

            postServices.deletePost(result);
            _context.Posts.Remove(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }

    }
}