using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.ProjectDTOs;

public class GetProjectWithTeamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Projectga bog‘langan team
    public List<GetUserDto> TeamMembers { get; set; } = new();
}
