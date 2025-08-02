using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.FOLLOW
{
    public class FollowRepository : IFollowRepository
    {
        public Task<List<User>> GetFollowersAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetFollowingAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsFollowingAsync(Guid followerId, Guid followeeId)
        {
            throw new NotImplementedException();
        }
    }
}
