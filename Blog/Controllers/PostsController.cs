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

        // GET
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

            postRes.postsCount = result.Count;
            postRes.blogPosts = result;
            return postRes;
        }


        [HttpGet("{slug}")]
        public async Task<IActionResult> GetPost([FromRoute] String slug)
        {
            PostResponseSingle postRes = new PostResponseSingle();
            try
            {
                // If post with entered slug doesn't exist in DB
                var result = await _context.Posts.FirstOrDefaultAsync(e => e.slug == slug);
                if (result == null)
                {
                    throw new SystemException("Post with selected slug doesn't exist!");
                }

                // Including tagList in response
                var post = _context.Posts
                    .Include(i => i.tagList)
                    .First(x => x.slug == slug);
                postRes.blogPost = post;
            }
            catch (SystemException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(postRes);
        }

       
        // PUT
        [HttpPut("{slug}")]
        public async Task<IActionResult> UpdatePost([FromRoute] String slug, [FromBody] PutRequest postReq)
        {
            Post entityPost = new Post();
            PostResponseSingle response = new PostResponseSingle();

            try
            {
                PostService postServices = new PostService(_context);
                if (slug !=postReq.blogPost.slug)
                {
                    throw new SystemException("Post with selected slug doesn't exist!");
                }

                entityPost = postServices.updatePost(slug, postReq.blogPost);
                response.blogPost = entityPost;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postReq)
        {
            Post newPost = new Post();
            PostResponseSingle response = new PostResponseSingle();

            try
            {
                PostService postServices = new PostService(_context);
                newPost = postServices.createPost(postReq.blogPost);
                response.blogPost = newPost;
            }
            catch (Exception e)
            {
                return BadRequest("Some error happened, please try again!");
            }

            return CreatedAtAction("GetPost", new { slug = postReq.blogPost.slug }, response);
        }

        // DELETE
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeletePost([FromRoute] String slug)
        {
            try
            {
                PostService postServices = new PostService(_context);
                var result = await _context.Posts.FirstOrDefaultAsync(e => e.slug == slug);

                if (result == null)
                {
                    throw new SystemException("Post with selected slug '" + slug + "' doesn't exist");
                }
                postServices.deletePost(result);
            }
            catch (SystemException e)
            {
                return BadRequest(e.Message);
            }

            return Ok("Post with slug '" + slug + "' successfully deleted!");
        }
    }
}