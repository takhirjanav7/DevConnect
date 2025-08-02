using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.COMMENT
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByPostAsync(Guid postId);
        Task<List<Comment>> GetMostLikedCommentsAsync(Guid postId);
        Task<bool> HasUserCommentedAsync(Guid userId, Guid postId);

    }
}
 