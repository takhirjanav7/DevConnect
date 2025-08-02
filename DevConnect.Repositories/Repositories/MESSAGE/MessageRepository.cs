using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.MESSAGE
{
    public class MessageRepository : IMessageRepository
    {
        public Task<List<Message>> GetConversationAsync(Guid userId1, Guid userId2)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetLatestMessageAsync(Guid conversationId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasUnreadMessagesAsync(Guid userId1)
        {
            throw new NotImplementedException();
        }
    }
}
