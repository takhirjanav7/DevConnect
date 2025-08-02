using DevConnect.BLL.DTOs.SkillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.SkillServices
{
    public interface ISkillService
    {
        Task<List<GetSkillDto>> GetAllAsync();
        Task<GetSkillDto> GetByIdAsync(Guid id);
        Task CreateAsync(CreateSkillDto dto);   
        Task UpdateAsync(Guid id, UpdateSkillDto dto);
        Task DeleteAsync(Guid id);

    }
}
