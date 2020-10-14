using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;

namespace Blog.Services
{
    public class Validation
    {
       /* private readonly BlogContext _context;

        public Validation(BlogContext context)
        {
            _context = context;
        }*/

        public String validationMessagePost(Post post)
        {
            // Required fields: title, description, body
            // Optional fields: tagList as an array of strings

            StringBuilder errorMessage = new StringBuilder();
            if (post.title == null) errorMessage.Append("Title field is required" + Environment.NewLine);
            if (post.description == null) errorMessage.Append("Description field is required"+ Environment.NewLine);
            if (post.body == null) errorMessage.Append("Body field is required");
            return errorMessage.ToString();
        }


        public String validationMessagePut(Post post)
        {
            // Optional fields: title, description, body
            String errorMessage = String.Empty;
            return errorMessage;
        }
    }
}
