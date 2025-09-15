using AutoMapper;
using DevConnect.BLL.DTOs.SkillDTOs;
using DevConnect.BLL.Services.SkillServices;
using DevConnect.Domain.Entities;

namespace DevConnect.Endpoints;

public static class SkillEndpoints
{
    public static void MapSkillEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Barcha skill'larni olish
        var group = app.MapGroup("/api/skills").WithTags("Skills");

        group.MapGet("/api/skills", async (ISkillService skillService) =>
        {
            var skills = await skillService.GetAllAsync();
            return Results.Ok(skills);
        });


        // 2. Skill ID bo'yicha olish
        group.MapGet("/api/skills/{id}", async (Guid id, ISkillService skillService) =>
        {
            var skill = await skillService.GetByIdAsync(id);
            return skill is not null ? Results.Ok(skill) : Results.NotFound($"Skill with ID '{id}' not found.");
        });


        // 3. Yangi skill qo'shish
        group.MapPost("/api/skills", async (CreateSkillDto dto, ISkillService skillService) =>
        {
            await skillService.CreateAsync(dto);
            return Results.Ok(dto);
        });


        // 4. Skill'ni yangilash
        group.MapPut("/api/skills/{id:guid}", async (Guid id, UpdateSkillDto dto, ISkillService skillService) =>
        {
            try
            {
                await skillService.UpdateAsync(id, dto);
                return Results.Ok(dto);
            }
            catch
            {
                return Results.NotFound(new { Message = $"Skill with ID {id} not found." });
            }
        });


        // 5. Skill'ni o'chirish
        group.MapDelete("/api/skills/{id:guid}", async (Guid id, ISkillService skillService) =>
        {
            try
            {
                await skillService.DeleteAsync(id);
                return Results.Ok();
            }
            catch
            {
                return Results.NotFound(new { Message = $"Skill with ID {id} not found." });
            }
        });


        // 6. Eng mashhur skill'lar
        group.MapGet("/api/skills/popular/{count:int}", async (int count, ISkillService skillService) =>
        {
            var skills = await skillService.GetPopularSkillsAsync(count);
            return Results.Ok(skills);
        });


        // 7.Skill va unga ega bo'lganlar userlar
        group.MapGet("/api/skills/{skillName}/users", async (string skillName, ISkillService skillService) =>
        {
            var skill = await skillService.GetSkillWithUsersAsync(skillName);
            return skill is not null ? Results.Ok(skill) : Results.NotFound($"Skill with name '{skillName}' not found.");
        });


        // 8. Userda shu skill bormi?
        group.MapGet("/api/skills/check/{userId:guid}/{skillName}", async (string skillName, Guid userId, ISkillService skillService) =>
        {
            var exists = await skillService.IsSkillLinkedToUserAsync(userId, skillName);
            return Results.Ok(new { UserId = userId, Skill = skillName, HasSkill = exists });
        });
    } 
}
