using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.UserServices
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetAllAsync();
        Task<GetUserDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateUserDto dto);
        Task UpdateAsync(Guid id, UpdateUserDto dto);
        Task DeleteAsync(Guid id);
        Task<bool> MakeUserAdminAsync(string username);
        Task<object?> GetByEmailAsync(string email);
        Task<object?> GetByUsernameAsync(string username);
        Task<List<GetUserDto>?> GetUsersBySkillAsync(string skillName);
        Task<object?> GetTopRatedUsersAsync(int count);
        Task<object?> GetUsersWithProjectsAsync();
        Task<bool> IsUsernameTakenAsync(string username);
    }
}
