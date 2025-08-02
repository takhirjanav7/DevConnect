using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.MessageDTOs
{
    public class GetMessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
