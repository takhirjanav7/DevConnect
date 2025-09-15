using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Domain.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public Guid RecipientId { get; set; } 
    public User Receiver { get; set; }
}
