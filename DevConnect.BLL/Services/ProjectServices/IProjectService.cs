using DevConnect.BLL.DTOs.ProjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<List<GetProjectDto>> GetAllAsync();
        Task<GetProjectDto> GetByIdAsync(Guid id);
        Task CreateAsync(CreateProjectDto dto);
        Task UpdateAsync(Guid id, UpdateProjectDto dto);
        Task DeleteAsync(Guid id);
    }
}
