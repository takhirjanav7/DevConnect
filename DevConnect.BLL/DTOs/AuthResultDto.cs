using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs;

public class AuthResultDto
{
    public bool Success { get; set; } 
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; } 
    public GetUserDto? User { get; set; }
    public string? Message { get; set; }
    public DateTime ExpiresAt { get; set; } 
} 
