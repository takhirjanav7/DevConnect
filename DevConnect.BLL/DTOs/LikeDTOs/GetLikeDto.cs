using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.LikeDTOs
{
    public class GetLikeDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Username { get; set; }

    }
}
