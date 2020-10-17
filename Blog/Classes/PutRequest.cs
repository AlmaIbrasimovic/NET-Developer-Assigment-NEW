using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.DTO;
using Blog.Models;

namespace Blog.Classes
{
    public class PutRequest
    {
        public PostDTO_PUT blogPost { get; set; }
    }
}
