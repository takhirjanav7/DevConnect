using DevConnect.BLL.DTOs.MessageDTOs;
using DevConnect.BLL.Services.MessageServices;

namespace DevConnect.Endpoints;

public static class MessageEndpoints
{
    public static void MapMessageEndpoints(this WebApplication app)
    {
        // 1. Yangi xabar yuborish
        var group = app.MapGroup("/api/messages").WithTags("Message");

        group.MapPost("/api/messages/{senderId:guid}", async (Guid senderId, CreateMessageDto dto, IMessageService service) =>
        {
            await service.SendMessageAsync(senderId, dto);
            return Results.Ok(new { Message = "Message sent successfully." });
        });


        // 2. Ikki foydalanuvchi orasidagi suhbatni olish
        group.MapGet("/api/messages/conversation/{user1Id:guid}/{user2Id:guid}", async (Guid user1Id, Guid user2Id, IMessageService service) =>
        {
            var messages = await service.GetConversationAsync(user1Id, user2Id);
            return Results.Ok(messages);
        });


        // 3. Xabarni o‘qilgan deb belgilash
        group.MapPut("/api/messages/{messageId:guid}/read", async (Guid messageId, IMessageService service) =>
        {
            await service.UpdateReadStatusAsync(messageId, true);
            return Results.Ok(new { Message = $"Message {messageId} marked as read." });
        });


        // 4. Conversation bo‘yicha eng so‘nggi xabarlarni olish
        group.MapGet("/api/messages/latest/{conversationId:guid}", async (Guid conversationId, IMessageService service) =>
        {
            var latest = await service.GetLatestMessageAsync(conversationId);
            return Results.Ok(latest);
        });


        // 5. Foydalanuvchida o‘qilmagan xabarlar bor-yo‘qligini tekshirish
        group.MapGet("/api/messages/unread/{userId:guid}", async (Guid userId, IMessageService service) =>
        {
            var hasUnread = await service.HasUnreadMessagesAsync(userId);
            return Results.Ok(new { HasUnread = hasUnread });
        });
    }
}
