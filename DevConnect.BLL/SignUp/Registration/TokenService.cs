using DevConnect.BLL.SignUp.Registration;
using DevConnect.DataAccess;
using DevConnect.DataAccess.Entities;
using DevConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private readonly ApplicationDbContext Context;
    private readonly IConfiguration Config;

    public TokenService(ApplicationDbContext context, IConfiguration configuration)
    {
        Context = context;
        Config = configuration;
    }

    public async Task DeleteRefreshTokenAsync(Guid userId)
    {
        var token = Context.RefreshTokens.Where(rt => rt.UserId == userId);
        Context.RefreshTokens.RemoveRange(token);
        await Context.SaveChangesAsync();
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Config["JwtSettings:Secret"]);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.Username ?? ""),
            new Claim(ClaimTypes.Role, user.Role ?? "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User> GetUserByRefreshTokenAsync(string token)
    {
        var refreshToken = await Context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.RefreshToken == token);

        return refreshToken?.User;
    }

    public async Task RemoveRefreshTokenAsync(Guid userid)
    {
        var token = await Context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userid);
        if (token != null)
        {
            Context.RefreshTokens.Remove(token);
            await Context.SaveChangesAsync();
        }
    }

    public async Task SaveRefreshTokenAsync(Guid userId, string token)
    {
        var existingTokens = Context.RefreshTokens.Where(rt => rt.UserId == userId);
        Context.RefreshTokens.RemoveRange(existingTokens);

        var refreshToken = new RefreshTokens
        {
            UserId = userId,
            RefreshToken = token,
            Expiration = DateTime.UtcNow.AddDays(7)
        };
        Context.RefreshTokens.Add(refreshToken);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> ValidateRefreshTokenAsync(string token)
    {
        return await Context.RefreshTokens.AnyAsync(rt =>
            rt.RefreshToken == token && rt.Expiration > DateTime.UtcNow);
    }
}