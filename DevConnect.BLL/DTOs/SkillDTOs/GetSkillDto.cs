using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.SkillDTOs
{
    public class GetSkillDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;

        public ICollection<GetUserDto> Users { get; set; } = new List<GetUserDto>();
    }
}
