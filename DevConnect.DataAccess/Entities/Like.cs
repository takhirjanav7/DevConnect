using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public Guid PostId { get; set; }
        public Post Post { get; set; } = new Post();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
