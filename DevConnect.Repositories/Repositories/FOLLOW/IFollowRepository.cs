using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.FOLLOW
{
    public interface IFollowRepository
    {
        Task<bool> IsFollowingAsync(Guid followerId, Guid followeeId);
        Task<List<User>> GetFollowersAsync(Guid userId);
        Task<List<User>> GetFollowingAsync(Guid userId);
    }
}
