using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.FollowDTOs
{
    public class GetFollowDto
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FollowingId { get; set; }
        public DateTime FollowedAt { get; set; }
    }
}
