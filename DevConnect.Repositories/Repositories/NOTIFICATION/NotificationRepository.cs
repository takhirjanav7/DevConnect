using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Repositories.Repositories.NOTIFICATION
{
    public class NotificationRepository : INotificationRepository
    {
        public Task<int> CountUnreadAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetUnreadNotificationsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task MarkAllAsReadAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
