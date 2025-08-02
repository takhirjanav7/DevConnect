using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.NotificationDTOs
{
    public class CreateNotificationDto
    {
        public Guid RecipientId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? RelatedEntityId { get; set; }
    }
}
