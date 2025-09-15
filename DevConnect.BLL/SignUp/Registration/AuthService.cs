using DevConnect.BLL.DTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories.USER;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.SignUp.Registration;

public class AuthService : IAuthService
{
    public readonly IUserRepository UserRepository;
    public readonly ITokenService TokenService;
    public readonly IPasswordHasher PasswordHasher;
    public readonly ILogger<AuthService> Logger;

    public AuthService(
        IUserRepository userRepository, 
        ITokenService tokenService, 
        IPasswordHasher passwordHasher, 
        ILogger<AuthService> logger)
    {
        UserRepository = userRepository;
        TokenService = tokenService;
        PasswordHasher = passwordHasher;
        Logger = logger;
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        if (await UserRepository.IsUsernameTakenAsync(dto.Username))
            throw new Exception("User already taken");

        var existingUser = await UserRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
            throw new Exception("Email already registered");

        var hashedPassword = PasswordHasher.Hash(dto.Password);

        // Birinchi foydalanuvchi Super Admin bo‘lsin
        string role = "User";
        var allUsers = await UserRepository.GetAllAsync();
        if (!allUsers.Any())
        {
            role = "Super Admin";
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            FullName = dto.FullName,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            Role = role
        };

        await UserRepository.CreateAsync(user);
        Logger.LogInformation("New user registered: {Email} with role {Role}", dto.Email, role);
    }


    public async Task<LogOutResultDto> LoginAsync(LoginUserDto dto)
    {
        var user = await UserRepository.GetByEmailAsync(dto.UsernameOrEmail);
        if (user == null)
        {
            user = await UserRepository.GetByUsernameAsync(dto.UsernameOrEmail);
        }


        if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
        {
            throw new Exception("Invalid username/email or password");
        }

        var token = TokenService.GenerateToken(user);
        var refreshToken = TokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await UserRepository.UpdateAsync(user);

        Logger.LogInformation("User logged in: {EmailOrUsername}", dto.UsernameOrEmail);

        return new LogOutResultDto
        {
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenDto> RefreshTokenAsync(string refreshToken) 
    {
        var user = await TokenService.GetUserByRefreshTokenAsync(refreshToken);
        if (user == null || !await TokenService.ValidateRefreshTokenAsync(refreshToken))
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var newAccessToken = TokenService.GenerateToken(user);
        var newRefreshToken = TokenService.GenerateRefreshToken();
        await TokenService.SaveRefreshTokenAsync(user.Id, newRefreshToken);

        Logger.LogInformation("Refresh token used: {Email}", user.Email);

        return new TokenDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
