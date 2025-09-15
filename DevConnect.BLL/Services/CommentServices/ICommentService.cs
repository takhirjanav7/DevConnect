using DevConnect.BLL.DTOs.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.CommentServices;

public interface ICommentService
{
    Task<List<GetCommentDto>> GetByPostIdAsync(Guid postId);
    Task CreateAsync(CreateCommentDto dto);
    Task UpdateAsync(Guid id, UpdateCommentDto dto);
    Task DeleteAsync(Guid id);
    Task<List<GetCommentDto>?> GetMostLikedCommentsAsync(Guid postId);
    Task<bool> HasUserCommentedAsync(Guid userId, Guid postId);
}
