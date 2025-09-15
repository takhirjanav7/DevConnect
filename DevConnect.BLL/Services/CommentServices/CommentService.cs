using AutoMapper;
using DevConnect.BLL.DTOs.CommentDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.CommentServices;

public class CommentService : ICommentService
{
    private readonly IGenericRepository<Comment> Repository;
    private readonly IMapper Mapper;

    public CommentService(IGenericRepository<Comment> repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    public async Task<List<GetCommentDto>> GetByPostIdAsync(Guid postId) =>
        Mapper.Map<List<GetCommentDto>>(await Repository.GetByPostIdAsync(postId));

    public async Task CreateAsync(CreateCommentDto dto)
    {
        var comment = Mapper.Map<Comment>(dto);
        comment.CreatedAt = DateTime.UtcNow;
        await Repository.CreateAsync(comment);
    }

    public async Task UpdateAsync(Guid id, UpdateCommentDto dto)
    {
        var comment = await Repository.GetByIdAsync(id);
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }
        Mapper.Map(dto, comment);
        await Repository.UpdateAsync(comment);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }

    public async Task<List<GetCommentDto>?> GetMostLikedCommentsAsync(Guid postId)
    {
        var comments = await Repository.GetAllAsync();
        var topComments = comments
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.LikesCount)
            .Take(5) // eng top 5 ta comment
            .ToList();

        if (!topComments.Any())
            return null;

        return Mapper.Map<List<GetCommentDto>>(topComments);
    }


    public async Task<bool> HasUserCommentedAsync(Guid userId, Guid postId)
    {
        var comments = await Repository.GetAllAsync();
        return comments.Any(c => c.AuthorId == userId && c.PostId == postId);
    }
}
