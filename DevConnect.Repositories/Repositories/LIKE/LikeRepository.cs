using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.LIKE;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    private readonly ApplicationDbContext Context;
    public LikeRepository(ApplicationDbContext context) : base(context)
    {
        Context = context;
    }

    public async Task<int> CountLikesByPostAsync(Guid postId)
    {
        return await Context.Likes
            .CountAsync(l => l.PostId == postId);
    }

    public async Task<bool> HasUserLikedPostAsync(Guid postId, Guid userId)
    {
        return await Context.Likes
            .AnyAsync(l => l.PostId == postId && l.UserId == userId);
    }

    Task<List<Like>> ILikeRepository.GetByPostIdAsync(Guid postId)
    {
        return Context.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }
}
