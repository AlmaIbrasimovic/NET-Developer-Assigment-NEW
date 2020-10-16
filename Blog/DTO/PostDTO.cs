using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.DTO
{
    public class PostDTO
    {
        
        // DTO class for POST endpoint for Post model
        public String slug { get; set; }

        [Required]
        public String title { get; set; }

        [Required]
        public String description { get; set; }

        [Required]
        public String body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public List<String> tagList { get; set; }
    }
}
