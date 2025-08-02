using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.CommentDTOs
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string AuthorUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
