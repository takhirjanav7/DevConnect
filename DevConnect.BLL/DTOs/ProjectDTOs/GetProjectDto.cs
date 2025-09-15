using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.ProjectDTOs;

public class GetProjectDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string? ImageUrl { get; set; } 
    public string? RepositoryUrl { get; set; }
}
