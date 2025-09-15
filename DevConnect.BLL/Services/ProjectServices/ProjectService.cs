using AutoMapper;
using DevConnect.BLL.DTOs.ProjectDTOs;
using DevConnect.BLL.DTOs.UserDTOs;
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

        public async Task<List<GetProjectDto>> GetProjectsByUserAsync(Guid userId)
        {
            // Barcha projectlarni olish
            var projects = await Repository.GetAllAsync();

            // Faqat userId mos keladigan projectlarni filtrlash
            var userProjects = projects.Where(p => p.UserId == userId).ToList();

            // DTO ga map qilish
            return Mapper.Map<List<GetProjectDto>>(userProjects);
        }

        public async Task<GetProjectDto?> GetRecentProjectsAsync(DateTime fromDate)
        {
            var recentProject = (await Repository.GetAllAsync())
                .Where(p => p.CreatedAt >= fromDate)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault();

            return Mapper.Map<GetProjectDto?>(recentProject);
        }

        public async Task<GetProjectWithTeamDto?> GetProjectWithTeamAsync(Guid projectId)
        {
            var project = await Repository.GetByIdAsync(projectId);
            if (project == null)
                return null;

            var dto = Mapper.Map<GetProjectWithTeamDto>(project);
            dto.TeamMembers = new List<GetUserDto>();

            if (project.User != null)
                dto.TeamMembers.Add(Mapper.Map<GetUserDto>(project.User));

            return dto;
        }

        public async Task<GetProjectDto?> GetTrendingProjectsAsync()
        {
            // LikeCount va CommentCount bo‘yicha eng mashhur projectni topish
            var projects = await Repository.GetAllAsync();
            var trendingProject = projects
                .OrderByDescending(p => p.LikeCount + p.CommentCount)
                .FirstOrDefault();

            return Mapper.Map<GetProjectDto?>(trendingProject);
        }
    }
}
