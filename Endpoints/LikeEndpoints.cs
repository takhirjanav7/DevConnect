using DevConnect.BLL.DTOs.LikeDTOs;
using DevConnect.BLL.Services.LikeServices;
using DevConnect.Domain.Entities;

namespace DevConnect.Endpoints;

public static class LikeEndpoints
{
    public static void MapLikeEndpoints(this WebApplication app)
    {
        // 1. Like qo'shish
        var group = app.MapGroup("/api/likes").WithTags("Like");

        group.MapPost("/api/likes", async (CreateLikeDto dto, ILikeService service) =>
        {
            await service.CreateAsync(dto);
            return Results.Ok(new { Message = "Like added successfully." });
        });


        // 2. Like o'chirish
        group.MapDelete("/api/likes/{likeId:guid}", async (Guid likeId, ILikeService service) =>
        {
            await service.DeleteAsync(likeId);
            return Results.Ok(new { Message = "Like removed successfully." });
        });


        // 3. Post bo'yicha barcha like'larni olish
        group.MapGet("/api/likes/post/{postId:guid}", async (Guid postId, ILikeService service) =>
        {
            var likes = await service.GetByPostIdAsync(postId);
            return Results.Ok(likes);
        });


        // 4.Postdagi like sonini olish
        group.MapGet("/api/likes/count/{postId:guid}", async (Guid postId, ILikeService service) =>
        {
            var count = await service.CountLikesByPostAsync(postId);
            return Results.Ok(new { LikeCount = count });
        });


        // 5. Foydalanuvchi postni like bosganmi tekshirish
        group.MapGet("/api/likes/{postId:guid}/user/{userId:guid}", async (Guid postId, Guid userId, ILikeService service) =>
        {
            var hasLiked = await service.HasUserLikedPostAsync(postId, userId);
            return Results.Ok(new { HasLiked = hasLiked });
        });
    }
}
