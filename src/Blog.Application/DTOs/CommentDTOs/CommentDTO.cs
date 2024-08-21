using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
