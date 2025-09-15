using DevConnect.BLL.DTOs.ProjectDTOs;
using DevConnect.BLL.Services.ProjectServices;

namespace DevConnect.Endpoints;

public static class ProjectEndpoints
{
    public static void MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Barcha projektlarni olish
        var group = app.MapGroup("/api/projects").WithTags("Projects");

        group.MapGet("/api/projects", async (IProjectService projectService) =>
        {
            var projects = await projectService.GetAllAsync();
            return Results.Ok(projects);
        });


        // 2.ID bo'yicha projektni olish
        group.MapGet("/api/projects/{id}", async (Guid id, IProjectService projectService) =>
        {
            var project = await projectService.GetByIdAsync(id);
            return project is not null ? Results.Ok(project) : Results.NotFound($"Project with ID {id} not found.");
        });


        // 3. Yangi projekt qo'shish
        group.MapPost("/api/projects", async (CreateProjectDto projectDto, IProjectService projectService) =>
        {
            await projectService.CreateAsync(projectDto);
            return Results.Ok(projectDto);
        });


        // 4. Projektni yangilash
        group.MapPut("/api/projects/{id}", async (Guid id, UpdateProjectDto projectDto, IProjectService projectService) =>
        {
            try
            {
                await projectService.UpdateAsync(id, projectDto);
                return Results.Ok(projectDto);
            }
            catch (Exception)
            {
                return Results.NotFound( new { Message = $"Project with ID {id} not found." });
            }
        });


        // 5. Projektni o'chirish
        group.MapDelete("/api/projects/{id}", async (Guid id, IProjectService projectService) =>
        {
            try
            {
                await projectService.DeleteAsync(id);
                return Results.Ok();
            }
            catch (Exception)
            {
                return Results.NotFound(new { Message = $"Project with ID {id} not found." });
            }
        });


        // 6. Foydalanuvchiga tegishli projektlarni olish
        group.MapGet("/api/projects/user/{userId}", async (Guid userId, IProjectService projectService) =>
        {
            var projects = await projectService.GetProjectsByUserAsync(userId);
            return Results.Ok(projects);
        });


        // 7. So'nggi projektlarni olish
        group.MapGet("/api/projects/recent/{fromDate}", async (DateTime fromDate, IProjectService projectService) =>
        {
            var projects = await projectService.GetRecentProjectsAsync(fromDate);
            return Results.Ok(projects);
        });


        // 8. Project va uning jamoasini olish
        group.MapGet("/api/projects/team/{projectId}", async (Guid projectId, IProjectService projectService) =>
        {
            var project = await projectService.GetProjectWithTeamAsync(projectId);
            return project is not null ? Results.Ok(project) : Results.NotFound($"Project with ID {projectId} not found.");
        });


        // 9. Trenddagi projektlarni olish
        group.MapGet("/api/projects/trending", async (IProjectService projectService) =>
        {
            var projects = await projectService.GetTrendingProjectsAsync();
            return Results.Ok(projects);
        });
    }
}
