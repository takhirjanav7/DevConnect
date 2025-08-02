using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.COMMENT
{
    public class CommentRepository : ICommentRepository
    {
        public Task<List<Comment>> GetCommentsByPostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetMostLikedCommentsAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasUserCommentedAsync(Guid userId, Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
