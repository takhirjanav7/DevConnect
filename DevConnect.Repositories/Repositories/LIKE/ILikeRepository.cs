using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.LIKE;

public interface ILikeRepository : IGenericRepository<Like>
{
    Task<List<Like>> GetByPostIdAsync(Guid postId);
    Task<bool> HasUserLikedPostAsync(Guid postId, Guid userId);
    Task<int> CountLikesByPostAsync(Guid postId);

}
