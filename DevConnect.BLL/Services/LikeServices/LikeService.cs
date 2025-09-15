using AutoMapper;
using DevConnect.BLL.DTOs.LikeDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.LikeServices;

public class LikeService : ILikeService
{
    private readonly IGenericRepository<Like> Repository;
    private readonly IMapper Mapper;

    public LikeService(IGenericRepository<Like> repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    public async Task CreateAsync(CreateLikeDto dto)
    {
        var like = Mapper.Map<Like>(dto);
        await Repository.CreateAsync(like);
    }

    public async Task<List<GetLikeDto>> GetByPostIdAsync(Guid postId) =>
        Mapper.Map<List<GetLikeDto>>(await Repository.GetByPostIdAsync(postId));

    public async Task DeleteAsync(Guid id) =>
        await Repository.DeleteAsync(id);

    public async Task<int> CountLikesByPostAsync(Guid postId)
    {
        var likes = await Repository.GetAllAsync();
        return likes.Count(l => l.PostId == postId);
    }


    public async Task<bool> HasUserLikedPostAsync(Guid postId, Guid userId)
    {
        var likes = await Repository.GetAllAsync();
        return likes.Any(l => l.PostId == postId && l.UserId == userId);
    }

}
