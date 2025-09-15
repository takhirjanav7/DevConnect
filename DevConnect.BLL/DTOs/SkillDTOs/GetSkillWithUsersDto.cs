using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.SkillDTOs;

public class GetSkillWithUsersDto
{
    public Guid Id { get; set; }          
    public string Name { get; set; } = ""; 
    public List<GetUserDto> Users { get; set; } = new();
}
