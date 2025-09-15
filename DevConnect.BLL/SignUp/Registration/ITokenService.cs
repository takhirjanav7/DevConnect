using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.SignUp.Registration;

public interface ITokenService
{
    Task SaveRefreshTokenAsync(Guid userId, string token);
    Task<bool> ValidateRefreshTokenAsync(string token);
    Task<User?> GetUserByRefreshTokenAsync(string token);
    Task DeleteRefreshTokenAsync(Guid userId);
    string GenerateToken(User user);
    string GenerateRefreshToken();
    Task RemoveRefreshTokenAsync(Guid userid);
}