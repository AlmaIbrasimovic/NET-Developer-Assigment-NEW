using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data
{
    public static class DbInitializer
    {
        
        public static String Slugify(String title)
        {
            String slug = title.Replace("%20", "-").ToLower();
            slug = Regex.Replace(slug, @"[^A-Za-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            return slug;
        }

        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();
            if (context.Posts.Any() && context.Tags.Any())
            {
                return;
            };

            var posts = new Post[]
            {
                new Post{
                    slug = Slugify("Augmented Reality iOS Application"),
                    title = "Augmented Reality iOS Application",
                    description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "iOS"},
                        new Tag() {name = "AR"},
                    },
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },

                new Post{
                    slug = Slugify("Augmented Reality iOS Application 2"),
                    title = "Augmented Reality iOS Application 2",
                    description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "iOS"},
                        new Tag() {name = "AR"},
                        new Tag() {name = "Gazzda"},
                    },
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },

                new Post{
                    slug = Slugify("Augmented Reality Android Application"),
                    title = "Augmented Reality Android Application",
                    description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "Android"},
                        new Tag() {name = "AR"},
                    },
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },
            };

            foreach (Post p in posts)
            {
                context.Posts.Add(p);
            } 
            context.SaveChanges();
        }
    }
}
