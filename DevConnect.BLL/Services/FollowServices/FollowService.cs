using AutoMapper;
using DevConnect.BLL.DTOs.FollowDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.FollowServices;

public class FollowService : IFollowService
{
    private readonly IGenericRepository<Follow> Repository;
    private readonly IMapper Mapper;

    public FollowService(IGenericRepository<Follow> repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    public async Task FollowAsync(Guid followerId, CreateFollowDto dto)
    {
        var follow = new Follow
        {
            FollowerId = followerId,
            FollowingId = dto.FollowingId,
            FollowedAt = DateTime.UtcNow
        };
        await Repository.CreateAsync(follow);
    }

    public async Task<List<GetFollowDto>> GetFollowersAsync(Guid userId)
    {
        return Mapper.Map<List<GetFollowDto>>(await Repository.GetFollowersAsync(userId));
    }

    public async Task<List<GetFollowDto>> GetFollowingAsync(Guid userId)
    {
        return Mapper.Map<List<GetFollowDto>>(await Repository.GetFollowingAsync(userId));
    }

    public async Task UnfollowAsync(Guid followerId, Guid followingId) =>
        await Repository.DeleteAsync(followerId, followingId);
}
