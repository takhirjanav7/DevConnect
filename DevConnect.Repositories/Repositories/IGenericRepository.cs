using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id, Guid followingId);
    Task SaveChangesAsync();
    Task<List<Notification>> GetByRecipientIdAsync(Guid userId);
    Task UpdateReadStatusAsync(Guid messageId, bool isRead);
    Task<object> GetConversationAsync(Guid user1Id, Guid user2Id);
    Task<object> GetByPostIdAsync(Guid postId);
    Task<object> GetFollowersAsync(Guid userId);
    Task<object> GetFollowingAsync(Guid userId);
    Task DeleteAsync(Guid id);
}
