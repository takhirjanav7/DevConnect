using DevConnect.BLL.DTOs;
using DevConnect.BLL.Services.UserServices;
using DevConnect.BLL.SignUp.Registration;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories.USER;

namespace DevConnect.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        // 0. Make user Admin endpoint
        group.MapPut("/api/users/{username}/make-admin", async (string username, IUserService userService) =>
        {
            var result = await userService.MakeUserAdminAsync(username);
            return result ? Results.Ok(new { message = $"{username} is now an Admin." })
                          : Results.NotFound(new { message = "User not found." });
        })
        .RequireAuthorization(policy => policy.RequireRole("Super Admin"));


        // 1. Register endpoint
        group.MapPost("/api/auth/register", async (RegisterUserDto dto, IUserRepository repo) =>
        {
            if (await repo.IsUsernameTakenAsync(dto.Username))
                return Results.BadRequest(new { message = "Username is already taken." });


            var existingUser = await repo.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
                return Results.BadRequest(new { message = "Email is already registered." });



            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await repo.AddUserAsync(user);
            await repo.SaveChangesAsync();

            return Results.Ok(new { message = "User registered successfully.", userId = user.Id });
        });


        // 2. Login endpoint
        group.MapPost("/api/auth/login", async (LoginUserDto dto, IUserRepository repo, ITokenService tokenService) =>
        {
            var user = await repo.GetByEmailAsync(dto.UsernameOrEmail);
            if (user == null)
            {
                user = await repo.GetByUsernameAsync(dto.UsernameOrEmail);
            }


            if (user is null)
                return Results.BadRequest(new { message = "User not found." });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Results.BadRequest(new { message = "Invalid password." });

            var accessToken = tokenService.GenerateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();

            await tokenService.SaveRefreshTokenAsync(user.Id, refreshToken);

            return Results.Ok(new
            {
                message = "Login successful.",
                accessToken,
                refreshToken,
                userId = user.Id,
                username = user.Username
            });
        });


        // 3. Refresh token endpoint
        group.MapPost("/api/auth/refresh", async (TokenDto refreshToken, ITokenService tokenService) =>
        {
            var isValid = await tokenService.ValidateRefreshTokenAsync(refreshToken.RefreshToken);
            if (!isValid)
            {
                return Results.BadRequest(new { message = "Invalid or expired refresh token." });
            }

            var user = await tokenService.GetUserByRefreshTokenAsync(refreshToken.RefreshToken);
            if (user == null)
            {
                return Results.BadRequest(new { message = "User not found." });
            }


            var newAccessToken = tokenService.GenerateToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken();


            await tokenService.SaveRefreshTokenAsync(user.Id, newRefreshToken);



            var result = new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return Results.Ok(result);
        });


        // 4. Logout endpoint
        group.MapPost("/logout", async (LogOutResultDto dto, ITokenService tokenService) =>
        {
            var user = await tokenService.GetUserByRefreshTokenAsync(dto.RefreshToken);
            if (user == null)
            {
                return Results.BadRequest(new { message = "Invalid refresh token." });
            }


            await tokenService.RemoveRefreshTokenAsync(user.Id);

            return Results.Ok(new { message = "Logged out successfully." });
        });
    }
}
