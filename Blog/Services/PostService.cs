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

        public Post updatePostWithTitle(String slug, Post newPost)
        {
            var oldPost = _context.Posts
                .Include(i => i.tagList)
                .First(x => x.slug == slug);

           if (newPost.body == null) newPost.body = oldPost.body;
           if (newPost.description == null) newPost.description = oldPost.description;
           if (newPost.createdAt == null) newPost.createdAt = oldPost.createdAt;
           newPost.slug = DbInitializer.Slugify(newPost.title);
           newPost.updatedAt = DateTime.Now;


           // Deleting old post
            _context.Posts.Remove(oldPost);

            //Adding new post
            _context.Posts.Add(newPost);
            _context.SaveChanges();

            return newPost;
        }

        public Post updatePost(String slug, Post newPost)
        {
            var oldPost = _context.Posts
                .Include(i => i.tagList)
                .First(x => x.slug == slug);

            if (newPost.createdAt != null) oldPost.createdAt = newPost.createdAt;
            if (newPost.tagList != null)
            {
                /*ICollection<Tag> tagList = new List<Tag>();
                if (post.tags != null)
                {
                    foreach (var tag in post.tags)
                    {
                        Tag newTag = new Tag() { name = tag };
                        tagList.Add(newTag);
                        _context.Tags.Add(newTag);
                        _context.SaveChanges();
                    }
                }
                newPost.tagList = tagList;*/
            }
            if (newPost.body != null) oldPost.body = newPost.body;
            if (newPost.description != null) oldPost.description = newPost.description;
            if (newPost.title != oldPost.title && newPost.title != null)
            {
               oldPost.slug = DbInitializer.Slugify(newPost.title);
               oldPost.title = newPost.title;
            }
            else if (newPost.title == oldPost.title && newPost.slug != null) oldPost.slug = newPost.slug;
            oldPost.updatedAt = DateTime.Now;

            return oldPost;
        }

        public Post copyPostDTO(PostDTO post)
        {
            Post newPost = new Post();
            newPost.title = post.title;
            newPost.body = post.body;
            newPost.createdAt = DateTime.Now;
            newPost.updatedAt = DateTime.Now;
            newPost.slug = DbInitializer.Slugify(post.title);
            newPost.description = post.description;
            ICollection<Tag> tagList = new List<Tag>();
            if (post.tags != null)
            {
                foreach (var tag in post.tags)
                {
                    Tag newTag = new Tag() {name = tag};
                    tagList.Add(newTag);
                    _context.Tags.Add(newTag);
                    _context.SaveChanges();
                }
            }
            newPost.tagList = tagList;
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
        }
    }
}
