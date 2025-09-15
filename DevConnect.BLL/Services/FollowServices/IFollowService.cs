using DevConnect.BLL.DTOs.FollowDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.FollowServices;

public interface IFollowService
{
    Task FollowAsync(Guid followerId, CreateFollowDto dto);
    Task<List<GetFollowDto>> GetFollowersAsync(Guid userId);
    Task<List<GetFollowDto>> GetFollowingAsync(Guid userId);
    Task<bool> IsFollowingAsync(Guid followerId, Guid followingId);
    Task UnfollowAsync(Guid followerId, Guid followingId);

}
