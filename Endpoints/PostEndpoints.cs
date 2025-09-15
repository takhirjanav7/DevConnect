using DevConnect.BLL.DTOs.PostDTOs;
using DevConnect.BLL.Services.PostServices;

namespace DevConnect.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this WebApplication app)
    {
        // 1. Barcha postlar
        var group = app.MapGroup("/api/posts").WithTags("Posts");

        group.MapGet("/api/posts", async (IPostService postService) =>
        {
            var posts = await postService.GetAllAsync();
            return Results.Ok(posts);
        });


        // 2. ID bo'yicha post
        group.MapGet("/api/posts/{id}", async (Guid id, IPostService postService) =>
        {
            var post = await postService.GetByIdAsync(id);
            return post is not null ? Results.Ok(post) : Results.NotFound($"Post with ID {id} not found.");
        });


        // 3. Post yaratish
        group.MapPost("/api/posts", async (CreatePostDto postDto, IPostService postService) =>
        {
            await postService.CreateAsync(postDto);
            return Results.Ok(postDto);
        });


        // 4. Postni yangilash
        group.MapPut("/api/posts/{id}", async (Guid id, UpdatePostDto postDto, IPostService postService) =>
        {
            try
            {
                await postService.UpdateAsync(id, postDto);
                return Results.Ok(postDto);
            }
            catch (Exception)
            {
                return Results.NotFound(new { Message = $"Post with ID {id} not found." });
            }
        });


        // 5. Postni o'chirish
        group.MapDelete("/api/posts/{id}", async (Guid id, IPostService postService) =>
        {
            try
            {
                await postService.DeleteAsync(id);
                return Results.Ok( new { Message = $"Post with ID {id} deleted succesfuly."});
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { Message = $"Post with ID {id} not found."});
            }
        });


        // 6. Foydalanuvchiga tegishli postlar
        group.MapGet("/api/users/{userId}/posts", async (Guid userId, IPostService postService) =>
        {
            var posts = await postService.GetPostsByUserAsync(userId);
            return Results.Ok(posts);
        });


        // 7. Foydalanuvchi feedidagi postlar
        group.MapGet("/api/users/feed/{userId}", async (Guid userId, IPostService postService) =>
        {
            var posts = await postService.GetFeedPostsAsync(userId);
            return Results.Ok(posts);
        });


        // 8. Postga tegishli kommentlar bilan birga olish
        group.MapGet("/api/posts/comments/{postId}", async (Guid postId, IPostService postService) =>
        {
            var post = await postService.GetPostsWithCommentsAsync(postId);
            return post is not null ? Results.Ok(post) : Results.NotFound($"Post with ID {postId} not found.");
        });


        // 9. Postlarni qidirish
        group.MapGet("/api/posts/search", async (string keyword, IPostService postService) =>
        {
            var posts = await postService.SearchPostsAsync(keyword);
            return Results.Ok(posts);
        });
    }
}
