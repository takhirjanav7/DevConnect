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

    }
}
