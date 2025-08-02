using AutoMapper;
using DevConnect.BLL.DTOs;
using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories.USER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper Mapper;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<AuthResultDto> LoginAsync(LoginUserDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Invalid credentials",
                ExpiresAt = DateTime.MinValue
            };
        }

        var getUserDto = Mapper.Map<GetUserDto>(user);
        var accessToken = _tokenService.CreateToken(getUserDto);
        var refreshToken = _tokenService.GenerateRefreshToken();

        return new AuthResultDto
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = getUserDto,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30) 
        };
    }

    public async Task<AuthResultDto> RefreshTokenAsync(string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(refreshToken);
        if (principal == null)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Invalid refresh token",
                ExpiresAt = DateTime.MinValue
            };
        }
        
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "User not found",
                ExpiresAt = DateTime.MinValue
            };
        }

        var getUserDto = Mapper.Map<GetUserDto>(user);
        var newAccessToken = _tokenService.CreateToken(getUserDto);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        return new AuthResultDto
        {
            Success = true,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            User = getUserDto,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30) 
        };
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterUserDto registerDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
        if(existingUser != null)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "User with this email already exists",
                ExpiresAt = DateTime.MinValue
            };
        }

        var user = new User
        {
            FullName = registerDto.FullName, 
            Username = registerDto.Username,
            Email = registerDto.Email,
            Role = registerDto.Role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            ProfileImageUrl = registerDto.ProfileImageUrl
        };


        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();


        var getUserDto = Mapper.Map<GetUserDto>(user);
        var accessToken = _tokenService.CreateToken(getUserDto);
        var refreshToken = _tokenService.GenerateRefreshToken();

        return new AuthResultDto
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = getUserDto,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30) 
        };
    }
}
