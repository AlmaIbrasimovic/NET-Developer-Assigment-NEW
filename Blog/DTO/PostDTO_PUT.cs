using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DTO
{
    public class PostDTO_PUT
    {
        public String slug { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public String body { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public List<String> tagList { get; set; }
    }
}
