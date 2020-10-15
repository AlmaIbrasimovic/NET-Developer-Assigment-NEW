using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext() {}

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Post>().ToTable("Post");
        }
    }
}
