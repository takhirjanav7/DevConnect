using DevConnect.BLL.DTOs;
using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.SignUp.Registration; 

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
