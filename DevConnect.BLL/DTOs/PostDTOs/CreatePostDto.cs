using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.PostDTOs
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
