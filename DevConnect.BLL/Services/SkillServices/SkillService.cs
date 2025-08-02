using AutoMapper;
using DevConnect.BLL.DTOs.SkillDTOs;
using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.SkillServices
{
    public class SkillService : ISkillService
    {
        private readonly IMapper Mapper;
        private readonly IGenericRepository<Skill> Repository;

        public SkillService(IMapper mapper, IGenericRepository<Skill> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public async Task<List<GetSkillDto>> GetAllAsync() =>
        Mapper.Map<List<GetSkillDto>>(await Repository.GetAllAsync());

        public async Task<GetSkillDto> GetByIdAsync(Guid id) =>
            Mapper.Map<GetSkillDto>(await Repository.GetByIdAsync(id));

        public async Task CreateAsync(CreateSkillDto dto)
        {
            var skill = Mapper.Map<Skill>(dto);
            await Repository.CreateAsync(skill);
        }

        public async Task UpdateAsync(Guid id, UpdateSkillDto dto)
        {
            var skill = await Repository.GetByIdAsync(id);
            if (skill == null)
            {
                throw new Exception($"Skill with ID {id} not found.");
            }
            Mapper.Map(dto, skill);
            await Repository.UpdateAsync(skill);
        }

        public async Task DeleteAsync(Guid id) =>
            await Repository.DeleteAsync(id);

    }
}
