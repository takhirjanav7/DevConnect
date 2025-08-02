using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.POST
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public readonly ApplicationDbContext Context;
        public PostRepository(ApplicationDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<List<Post>> GetFeedPostsAsync(Guid userId)
        {
            return await Context.Posts
                .Where(p => p.User.Followers.Any(f => f.FollowerId == userId))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUserAsync(Guid userId)
        {
            return await Context.Posts.Where(p => p.UserId == userId).ToListAsync();    
        }

        public async Task<Post> GetPostsWithCommentsAsync(Guid postId)
        {
            var post = await Context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                throw new Exception($"");
            }
            return post;
        }

        public Task<List<Post>> SearchPostsAsync(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
