using DevConnect.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs;

public class TokenDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } 
} 
