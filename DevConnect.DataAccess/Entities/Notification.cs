using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DevConnect.Domain.Entities;
public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? RelatedEntityId { get; set; }
    public bool IsRead { get; set; } = false;
    public Guid RecipientId { get; set; }
    public User Recipient { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }

}
