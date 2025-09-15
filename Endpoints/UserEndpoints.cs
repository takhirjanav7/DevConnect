using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.BLL.Services.UserServices;

namespace DevConnect.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // 1. Get all endpoints
        var group = app.MapGroup("/api/users").WithTags("Users");

        group.MapGet("/api/users", async (IUserService userService) =>
        {
            var users = await userService.GetAllAsync();
            return Results.Ok(users);
        });


        // 2. Get user by ID
        group.MapGet("/api/users/{id}", async (Guid id, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });


        //// 3. Create a new user
        //group.MapPost("/", async (CreateUserDto userDto, IUserService userService) =>
        //{
        //    await userService.CreateAsync(userDto);
        //    return Results.Created($"/api/users/{userDto.Username}", userDto);
        //});


        // 4. Update an existing user
        group.MapPut("/api/users/{id}", async (Guid id, UpdateUserDto userDto, IUserService userService) =>
        {
            try
            {
                await userService.UpdateAsync(id, userDto);
                return Results.Ok(userDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { Message = $"User with ID {id} not found." });
            }
        });


        // 5. Get user by email
        group.MapGet("/email/{email}", async (string email, IUserService userService) =>
        {
            var user = await userService.GetByEmailAsync(email);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });


        // 6. Get user by username
        group.MapGet("/api/users/username/{username}", async (string username, IUserService userService) =>
        {
            var user = await userService.GetByUsernameAsync(username);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });


        // 7. Get user by skill
        group.MapGet("/api/users/skill/{skillName}", async (string skillName, IUserService userService) =>
        {
            var users = await userService.GetUsersBySkillAsync(skillName);
            return Results.Ok(users);
        });


        // 8. Get top-rated users
        group.MapGet("/api/users/top-rated/{count:int}", async (int count, IUserService userService) =>
        {
            var users = await userService.GetTopRatedUsersAsync(count);
            return Results.Ok(users);
        });


        // 9. Get users with projects
        group.MapGet("/api/users/with-projects", async (IUserService userService) =>
        {
            var users = await userService.GetUsersWithProjectsAsync();
            return Results.Ok(users);
        });


        // 10. Get check if username is taken
        group.MapGet("/api/users/is-taken/{username}", async (string username, IUserService userService) =>
        {
            var isTaken = await userService.IsUsernameTakenAsync(username);
            return Results.Ok(new { Username = username, IsTaken = isTaken });
        });
    }
}
