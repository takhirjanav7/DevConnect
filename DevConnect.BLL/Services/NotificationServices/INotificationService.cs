using DevConnect.BLL.DTOs.NotificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.NotificationServices;

public interface INotificationService
{
    Task SendNotificationAsync(CreateNotificationDto dto);
    Task<List<GetNotificationDto>> GetUserNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(Guid notificationId);
    Task<List<GetNotificationDto>?> GetUnreadNotificationsAsync(Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
    Task<int> CountUnreadAsync(Guid userId);
}