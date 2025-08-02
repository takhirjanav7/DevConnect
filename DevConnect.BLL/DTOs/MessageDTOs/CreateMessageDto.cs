using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.MessageDTOs
{
    public class CreateMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid RecipientId { get; set; }
    }
}
