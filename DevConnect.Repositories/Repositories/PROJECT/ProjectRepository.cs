using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.PROJECT
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext Context;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<List<Project>> GetProjectsByUserAsync(Guid userId)
        {
            return await Context.Projects
                .Where(p => p.TeamMembers)
                .ToListAsync();
        }

        public async Task<Project> GetProjectWithTeamAsync(Guid projectId)
        {
            var project = await Context.Projects
                .Include(p => p.TeamMembers)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                throw new Exception($"Project with ID '{projectId}' not found.");
            }
            return project;
        }

        public Task<List<Project>> GetRecentProjectsAsync(DateTime fromDate)
        {
            return Context.Projects
                .Where(p => p.CreatedAt >= fromDate)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Project>> GetTrendingProjectsAsync()
        {
            return await Context.Projects
                .OrderByDescending(p => p.LikeCount + p.CommentCount) 
                .Take(10)
                .ToListAsync();
        }
    }
}
