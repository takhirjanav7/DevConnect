using AutoMapper;
using DevConnect.BLL.DTOs.MessageDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<Message> Repository;
        private readonly IMapper Mapper;

        public MessageService(IGenericRepository<Message> repo, IMapper mapper)
        {
            Repository = repo;
            Mapper = mapper;
        }

        public async Task SendMessageAsync(Guid senderId, CreateMessageDto dto)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = dto.RecipientId, 
                Text = dto.Text,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };
            await Repository.CreateAsync(message);
        }

        public async Task<List<GetMessageDto>> GetConversationAsync(Guid user1Id, Guid user2Id) =>
            Mapper.Map<List<GetMessageDto>>(await Repository.GetConversationAsync(user1Id, user2Id));

        public async Task UpdateReadStatusAsync(Guid messageId, bool isRead) =>
            await Repository.UpdateReadStatusAsync(messageId, isRead);

        public async Task<GetMessageDto?> GetLatestMessageAsync(Guid conversationId)
        {
            var messages = await Repository.GetAllAsync();
            var latest = messages
                .Where(m => m.SenderId == conversationId)
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefault();

            if (latest == null)
                return null;

            return Mapper.Map<GetMessageDto>(latest);
        }


        public async Task<bool> HasUnreadMessagesAsync(Guid userId)
        {
            var messages = await Repository.GetAllAsync();
            return messages.Any(m => m.RecipientId == userId && !m.IsRead);
        }
    }
}
