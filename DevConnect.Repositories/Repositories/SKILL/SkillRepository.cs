using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.SKILL
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly ApplicationDbContext Context;

        public SkillRepository(ApplicationDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<List<Skill>> GetPopularSkillsAsync(int topCount)
        {
            return await Context.Skills
                .OrderByDescending(s => s.Users.Count)
                .Take(topCount)
                .ToListAsync();
        }

        public async Task<Skill> GetSkillWithUsersAsync(string skillName)
        {
             var skill = await Context.Skills
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Name == skillName);


                if (skill == null)
                {
                    throw new Exception($"Skill with name '{skillName}' not found.");
                }
                return skill;
        }

        public async Task<bool> IsSkillLinkedToUserAsync(Guid userId, string skillName)
        {
            return await Context.Users
            .Where(u => u.Id == userId)
            .AnyAsync(u => u.Skills.Any(s => s.Name == skillName));
        }
    }
}
