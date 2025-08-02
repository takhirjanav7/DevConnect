using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.NOTIFICATION
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUnreadNotificationsAsync(Guid userId);
        Task MarkAllAsReadAsync(Guid userId);
        Task<int> CountUnreadAsync(Guid userId);
    }
}