using AutoMapper;
using DevConnect.BLL.DTOs.ProjectDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.ProjectServices
{
    public class ProjectService : IProjectService
    { 
        private readonly IMapper Mapper;
        private readonly IGenericRepository<Project> Repository;

        public ProjectService(IGenericRepository<Project> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<List<GetProjectDto>> GetAllAsync() =>
        Mapper.Map<List<GetProjectDto>>(await Repository.GetAllAsync());

        public async Task<GetProjectDto> GetByIdAsync(Guid id) =>
            Mapper.Map<GetProjectDto>(await Repository.GetByIdAsync(id));

        public async Task CreateAsync(CreateProjectDto dto)
        {
            var project = Mapper.Map<Project>(dto);
            await Repository.CreateAsync(project);
        }

        public async Task UpdateAsync(Guid id, UpdateProjectDto dto)
        {
            var project = await Repository.GetByIdAsync(id);
            if (project == null)
            {
                throw new Exception($"Project with ID {id} not found.");
            }
            Mapper.Map(dto, project);
            await Repository.UpdateAsync(project);
        }

        public async Task DeleteAsync(Guid id) =>
            await Repository.DeleteAsync(id);

    }
}
