using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.USER;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext Context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        Context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await Context.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetTopRatedUsersAsync(int count)
    {
        return await Context.Users
            .OrderByDescending(u => u.Rating)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersBySkillAsync(string skillName)
    {
        return await Context.Users
            .Where(u => u.Skills.Any(s => s.Name == skillName))
            .ToListAsync();
    }

    public async Task<List<User>> GetUsersWithProjectsAsync()
    {
        return await Context.Users
            .Where(u => u.Projects.Any())
            .ToListAsync();
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await Context.Users
            .AnyAsync(u => u.Username == username);
    }
}
