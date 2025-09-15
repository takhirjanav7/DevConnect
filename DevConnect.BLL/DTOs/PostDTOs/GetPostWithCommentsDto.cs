using DevConnect.BLL.DTOs.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.PostDTOs;

public class GetPostWithCommentsDto
{
    public GetPostDto Post { get; set; } = new();
    public IEnumerable<GetCommentDto> Comments { get; set; } = new List<GetCommentDto>();
}
