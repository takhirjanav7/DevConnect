using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.POST
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsByUserAsync(Guid userId);
        Task<List<Post>> GetFeedPostsAsync(Guid userId);
        Task<Post> GetPostsWithCommentsAsync(Guid postId);
        Task<List<Post>> SearchPostsAsync(string keyword);
    }
}
