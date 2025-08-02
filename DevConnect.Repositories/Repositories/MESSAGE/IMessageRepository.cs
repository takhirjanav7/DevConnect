using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.MESSAGE
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetConversationAsync(Guid userId1, Guid userId2);
        Task<List<Message>> GetLatestMessageAsync(Guid conversationId);
        Task<bool> HasUnreadMessagesAsync(Guid userId1);
    }
}
