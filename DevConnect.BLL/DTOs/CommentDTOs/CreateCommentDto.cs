using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.CommentDTOs
{
    public class CreateCommentDto
    {
        public Guid PostId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
