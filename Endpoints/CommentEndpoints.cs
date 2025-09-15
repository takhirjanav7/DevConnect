using DevConnect.BLL.DTOs.CommentDTOs;
using DevConnect.BLL.Services.CommentServices;

namespace DevConnect.Endpoints;

public static class CommentEndpoints
{
    public static void MapCommentEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Postga oid barcha kommentlarni olish
        var group = app.MapGroup("/api/comments").WithTags("Comments");

        group.MapGet("/api/comments/post/{postId:guid}", async (Guid postId, ICommentService commentService) =>
        {
            var comments = await commentService.GetByPostIdAsync(postId);
            return Results.Ok(comments);
        });


        // 2. Yangi komment yaratish
        group.MapPost("/api/comments", async (CreateCommentDto dto, ICommentService commentService) =>
        {
            await commentService.CreateAsync(dto);
            return Results.Ok(new { Message = "Comment created successfully"});
        });


        // 3. Kommentni yangilash
        group.MapPut("/api/comments/{id:guid}", async (Guid id, UpdateCommentDto dto, ICommentService commentService) =>
        {
            await commentService.UpdateAsync(id, dto);
            return Results.Ok(new { Message = $"Comment {id} updated successfully" });
        });


        // 4. Kommentni o'chirish
        group.MapDelete("/api/comments/{id:guid}", async (Guid id, ICommentService commentService) =>
        {
            await commentService.DeleteAsync(id);
            return Results.Ok(new { Message = $"Comment {id} deleted successfully" });
        });


        // 5.Postdagi eng ko'p like olingan kommentlarni olish
        group.MapGet("/api/comments/post/{postId:guid}/most-liked", async (Guid postId, ICommentService commentService) =>
        {
            var topComments = await commentService.GetMostLikedCommentsAsync(postId);
            return Results.Ok(topComments);
        });


        // 6. Foydalanuvchi shu postga comment yozganmi?
        group.MapGet("/api/comments/{postId:guid}/has-commented/{userId:guid}", async (Guid postId, Guid userId, ICommentService commentService) =>
        {
            var hasCommented = await commentService.HasUserCommentedAsync(userId, postId);
            return Results.Ok(new { UserId = userId, PostId = postId, HasCommented = hasCommented });
        });
    }
}
