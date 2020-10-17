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

            // Seeding DB
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

                new Post{
                    slug = Slugify("Virtual reality React Native Application"),
                    title = "Virtual reality React Native Application",
                    description = "With this app you will feel like you are there!",
                    body = "The app is simple to use, and will help you experience travelling on another level!",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "Android"},
                        new Tag() {name = "iOS"},
                        new Tag() {name = "ReactNative"},
                        new Tag() {name = "VR"},
                    },
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },

                new Post{
                    slug = Slugify("Internet Trends 2018"),
                    title = "Internet Trends 2018",
                    description = "Ever wonder how?",
                    body = "An opinionated commentary, of the most important presentation of the year",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "trends"},
                        new Tag() {name = "innovation"},
                        new Tag() {name = "2018"},
                    },
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },

                new Post{
                    slug = Slugify("React or Angular"),
                    title = "React or Angular",
                    description = "How to choose?",
                    body = "Detailed blog post on what to choose and why.",
                    tagList = new List<Tag>
                    {
                        new Tag() {name = "React"},
                        new Tag() {name = "Angular"}
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
