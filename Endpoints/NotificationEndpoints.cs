using DevConnect.BLL.DTOs.NotificationDTOs;
using DevConnect.BLL.Services.NotificationServices;
using DevConnect.Repositories.Repositories.NOTIFICATION;

namespace DevConnect.Endpoints;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this WebApplication app)
    {
        // 1. Yangi Notification yuborish
        var group = app.MapGroup("/api/notifications").WithTags("Notifications");

        group.MapPost("/api/notifications", async (CreateNotificationDto dto, INotificationService service) =>
        {
            await service.SendNotificationAsync(dto);
            return Results.Ok(new { Message = "Notification sent successfuly." });
        });


        // 2. Foydalanuvchining barcha Notificationlarini olish
        group.MapGet("/api/notifications/{userId:guid}", async (Guid userId, INotificationService service) =>
        {
            var notifications = await service.GetUserNotificationsAsync(userId);
            return Results.Ok(notifications);
        });


        // 3. Notificationni o'qilgan deb belgilash
        group.MapPost("/api/notifications/{id}/read", async (Guid id, INotificationService service) =>
        {
            await service.MarkAsReadAsync(id);
            return Results.Ok(new { Message = $"Notification {id} marked as read." });
        });


        // 4. Foydalanuvchining o‘qilmagan notificationlarini olish
        group.MapGet("/api/notifications/unread/{userId:guid}", async (Guid userId, INotificationService service) =>
        {
            var unread = await service.GetUnreadNotificationsAsync(userId);
            return Results.Ok(unread);
        });


        // 5, Foydalanuvchining barcha notificationlarini o‘qilgan qilish
        group.MapPut("/api/notifications/{userId}/mark-all-read", async (Guid userId, INotificationService service) =>
        {
            await service.MarkAllAsReadAsync(userId);
            return Results.Ok(new { Message = "All Notifications marked as read."});
        });


        // 6. O'qilmagan Notificationlarni sanash
        group.MapGet("/api/notifications/unread-count/{userId}", async (Guid userId, INotificationService service) =>
        {
            var count = await service.CountUnreadAsync(userId);
            return Results.Ok(new { UnreadCount = count });
        });
    }
}
