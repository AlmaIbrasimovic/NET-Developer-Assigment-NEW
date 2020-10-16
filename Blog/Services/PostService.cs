using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.DTO;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class PostService
    {
        private readonly BlogContext _context;

        public PostService(BlogContext context)
        {
            _context = context;
        }

        public Post updatePost(String slug, Post newPost)
        {
            var oldPost = _context.Posts
                .Include(i => i.tagList)
                .First(x => x.slug == slug);

            if (newPost.body != null) oldPost.body = newPost.body;
            if (newPost.description != null) oldPost.description = newPost.description;

            // We compare these two values to DateTime.Min value, which is default value, in case if we didn't send these values
            // we wouldn't update the values
            if (newPost.updatedAt != DateTime.MinValue) oldPost.updatedAt = newPost.updatedAt;
            if (newPost.createdAt != DateTime.MinValue) oldPost.createdAt = newPost.createdAt;

            if (newPost.title != oldPost.title && newPost.title != null)
            {
               oldPost.slug = DbInitializer.Slugify(newPost.title);
               oldPost.title = newPost.title;
            }
            else if (newPost.title == oldPost.title && newPost.slug != null) oldPost.slug = newPost.slug;

            // Updating old post
            _context.Update(oldPost);
            _context.SaveChanges();
            return oldPost;
        }

        public Post createPost(PostDTO post)
        {
            Post newPost = new Post();
            newPost.title = post.title;
            newPost.body = post.body;
            newPost.createdAt = post.createdAt;
            newPost.updatedAt = post.updatedAt;
            newPost.slug = DbInitializer.Slugify(post.title);
            newPost.description = post.description;
            ICollection<Tag> tagList = new List<Tag>();
            if (post.tagList != null)
            {
                foreach (var tag in post.tagList)
                {
                    Tag newTag = new Tag() {name = tag};
                    tagList.Add(newTag);
                    _context.Tags.Add(newTag);
                    _context.SaveChanges();
                }
            }
            newPost.tagList = tagList;
            _context.Posts.Add(newPost);
            _context.SaveChanges();
            return newPost;
        }

        public void deletePost(Post post)
        {
            var tagList = _context.Tags
                .FromSql("SELECT * FROM Tag WHERE Postguid = @p0", post.guid)
                .ToList();

            foreach (var tag in tagList)
            {
                _context.Tags.Remove(tag);
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
