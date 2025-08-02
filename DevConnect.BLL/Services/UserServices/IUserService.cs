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
    }
}
