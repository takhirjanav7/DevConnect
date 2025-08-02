using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.SKILL
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task<List<Skill>> GetPopularSkillsAsync(int topCount);
        Task<Skill> GetSkillWithUsersAsync(string skillName);
        Task<bool> IsSkillLinkedToUserAsync(Guid userId, string skillName);
    }
}
