using AutoMapper;
using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using DevConnect.Repositories.Repositories.USER;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> Repository;
        private readonly IMapper Mapper;

        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<List<GetUserDto>> GetAllAsync() =>
        Mapper.Map<List<GetUserDto>>(await Repository.GetAllAsync());

        public async Task<GetUserDto?> GetByIdAsync(Guid id) =>
            Mapper.Map<GetUserDto>(await Repository.GetByIdAsync(id));

        public async Task CreateAsync(CreateUserDto dto)
        {
            var user = Mapper.Map<User>(dto);
            await Repository.CreateAsync(user);
        }

        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }
            Mapper.Map(dto, user);
            await Repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id) =>
            await Repository.DeleteAsync(id);

        public async Task<bool> MakeUserAdminAsync(string username)
        {
            var user = (await Repository.GetAllAsync())
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false; 
            }

            user.Role = "Admin";

            await Repository.UpdateAsync(user);

            return true; 
        }

        public async Task<object?> GetByEmailAsync(string email)
        {
            var users = await Repository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            return Mapper.Map<GetUserDto>(user);
        }

        public async Task<object?> GetByUsernameAsync(string username)
        {
            var users = await Repository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return null;

            return Mapper.Map<GetUserDto>(user);
        }

        public async Task<List<GetUserDto>?> GetUsersBySkillAsync(string skillName)
        {
            var users = await Repository.GetAllAsync();

            var usersWithSkill = users
                .Where(u => u.Skills != null &&
                            u.Skills.Any(s =>
                                string.Equals(s.Name, skillName, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return Mapper.Map<List<GetUserDto>>(usersWithSkill);
        }

        public async Task<object?> GetTopRatedUsersAsync(int count)
        {
            var users = await Repository.GetAllAsync();
            var topUsers = users
                .OrderByDescending(u => u.Rating)
                .Take(count)
                .ToList();

            if (!topUsers.Any())
                return null;

            return Mapper.Map<List<GetUserDto>>(topUsers);
        }

        public async Task<object?> GetUsersWithProjectsAsync()
        {
            var users = await Repository.GetAllAsync();
            var usersWithProjects = users
                .Where(u => u.Projects != null && u.Projects.Any())
                .ToList();

            if (!usersWithProjects.Any())
                return null;

            return Mapper.Map<List<GetUserDto>>(usersWithProjects);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            var users = await Repository.GetAllAsync();
            return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
