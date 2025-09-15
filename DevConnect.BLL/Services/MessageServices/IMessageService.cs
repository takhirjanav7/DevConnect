using DevConnect.BLL.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.MessageServices;

public interface IMessageService
{
    Task SendMessageAsync(Guid senderId, CreateMessageDto dto);
    Task<List<GetMessageDto>> GetConversationAsync(Guid user1Id, Guid user2Id);
    Task UpdateReadStatusAsync(Guid messageId, bool isRead);
    Task<GetMessageDto?> GetLatestMessageAsync(Guid conversationId);
    Task<bool> HasUnreadMessagesAsync(Guid userId);
}