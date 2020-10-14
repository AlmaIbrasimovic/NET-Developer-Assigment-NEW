using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public String slug { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public String body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public virtual ICollection<Tag> tagList { get; set; }
    }
}
