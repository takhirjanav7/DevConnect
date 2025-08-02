using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.PROJECT
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsByUserAsync(Guid userId);
        Task<List<Project>> GetRecentProjectsAsync(DateTime fromDate);
        Task<Project> GetProjectWithTeamAsync(Guid projectId);
        Task<List<Project>> GetTrendingProjectsAsync();
    }
} 
