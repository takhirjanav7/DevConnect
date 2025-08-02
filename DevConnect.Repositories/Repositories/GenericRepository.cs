using DevConnect.DataAccess;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext Context;
        private readonly DbSet<T> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid followerId, Guid followingId)
        {
            if (followerId == Guid.Empty || followingId == Guid.Empty || followerId == followingId)
                throw new ArgumentException("Invalid follower or following ID");

            var follow = await Context.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (follow is null)
                throw new InvalidOperationException("Follow relationship does not exist");

            Context.Follows.Remove(follow);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await Context.Comments.FindAsync(id);
            if(comment is null)
            {
                throw new InvalidOperationException("Comment not found");
            }

            Context.Comments.Remove(comment);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<object> GetByPostIdAsync(Guid postId)
        {
            var likes = await Context.Likes
                .Where(l => l.PostId == postId)
                .Select(l => new
                {
                    l.Id,
                    l.UserId,
                    l.User.Username,
                    l.CreatedAt
                })
                .ToListAsync();

            return likes;
        }

        public async Task<List<Notification>> GetByRecipientIdAsync(Guid userId)
        {
            return await Context.Notifications
                .Where(n => n.RecipientId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<object> GetConversationAsync(Guid user1Id, Guid user2Id)
        {
            var messages = await Context.Messages
                .Where(m => (m.SenderId == user1Id && m.RecipientId == user2Id) ||
                            (m.SenderId == user2Id && m.RecipientId == user1Id))
                .OrderBy(m => m.SentAt)
                .Select(m => new
                {
                    m.Id,
                    m.SenderId,
                    m.RecipientId,
                    m.Text,
                    m.SentAt,
                    m.IsRead
                })
                .ToListAsync();

            return messages;
        }

        public async Task<object> GetFollowersAsync(Guid userId)
        {
            var followers = await Context.Follows
                .Where(f => f.FollowingId == userId)
                .Select(f => new
                {
                    f.FollowerId,
                    f.Follower.Username,
                    f.Follower.ProfileImageUrl
                })
                .ToListAsync();

            return followers;
        }

        public async Task<object> GetFollowingAsync(Guid userId)
        {
            var following = await Context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => new
                {
                    f.FollowingId,
                    f.Following.Username,
                    f.Following.ProfileImageUrl
                })
                .ToListAsync();

            return following;
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateReadStatusAsync(Guid messageId, bool isRead)
        {
            var message = await Context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message with ID {messageId} not found.");
            }

            message.IsRead = isRead;
            await Context.SaveChangesAsync();
        }
    }
}
