using DevConnect.BLL.DTOs.PostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.PostServices;
public interface IPostService
{
    Task<List<GetPostDto>> GetAllAsync();
    Task<GetPostDto> GetByIdAsync(Guid id);
    Task CreateAsync(CreatePostDto dto);
    Task UpdateAsync(Guid id, UpdatePostDto dto);
    Task DeleteAsync(Guid id);
}
