using DevConnect.BLL.DTOs.FollowDTOs;
using DevConnect.BLL.Services.FollowServices;

namespace DevConnect.Endpoints;

public static class FollowEndpoints
{
    public static void MapFollowEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Follow qilish
        var group = app.MapGroup("/api/follow").WithTags("Follow");

        group.MapPost("/api/follow/{followerId:guid}", async (Guid followerId, CreateFollowDto dto, IFollowService followService) =>
        {
            await followService.FollowAsync(followerId, dto);
            return Results.Ok(new { Message = "Successfully followed user." });
        });


        // 2. Unfollow qilish
        group.MapDelete("/api/follows/{followerId:guid}/{followingId:guid}", async (Guid followerId, Guid followingId, IFollowService followService) =>
        {
            await followService.UnfollowAsync(followerId, followingId);
            return Results.Ok(new { Message = "Successfully unfollowed user." });
        });


        // 3. Foydalanuvchining Followers (kimlar uni follow qilgan)
        group.MapGet("/api/follows/{userId:guid}/followers", async (Guid userId, IFollowService followService) =>
        {
            var followers = await followService.GetFollowersAsync(userId);
            return Results.Ok(followers);
        });


        // 4. Foydalanuvchining Following (u kimlarni follow qilgan)
        group.MapGet("/api/follows/{userId:guid}/following", async (Guid userId, IFollowService followService) =>
        {
            var following = await followService.GetFollowingAsync(userId);
            return Results.Ok(following);
        });


        // 5. Tekshirish: followerId user followingId ni follow qilganmi?
        group.MapGet("/api/follows/{followerId:guid}/is-following/{followingId:guid}", async (Guid followerId, Guid followingId, IFollowService followService) =>
        {
            var isFollowing = await followService.IsFollowingAsync(followerId, followingId);
            return Results.Ok(new { IsFollowing = isFollowing });
        });
    }
}
