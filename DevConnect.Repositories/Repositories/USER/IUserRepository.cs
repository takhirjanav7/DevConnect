using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.USER
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
        Task<List<User>> GetUsersBySkillAsync(string skillName);
        Task<List<User>> GetTopRatedUsersAsync(int count);
        Task<bool> IsUsernameTakenAsync(string username);
        Task<List<User>> GetUsersWithProjectsAsync();
    }
}