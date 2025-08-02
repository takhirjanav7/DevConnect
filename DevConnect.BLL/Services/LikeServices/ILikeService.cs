using DevConnect.BLL.DTOs.LikeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.LikeServices;

public interface ILikeService
{
    Task CreateAsync(CreateLikeDto dto);
    Task<List<GetLikeDto>> GetByPostIdAsync(Guid postId);
    Task DeleteAsync(Guid id);
}
