using AutoMapper;
using DevConnect.BLL.DTOs.NotificationDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.NotificationServices;

public class NotificationService : INotificationService
{
    private readonly IGenericRepository<Notification> Repository;
    private readonly IMapper Mapper;

    public NotificationService(IGenericRepository<Notification> repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    public async Task SendNotificationAsync(CreateNotificationDto dto)
    {
        var notification = new Notification
        {
            RecipientId = dto.RecipientId,
            Type = dto.Type,
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };
        await Repository.CreateAsync(notification);
    }

    public async Task<List<GetNotificationDto>> GetUserNotificationsAsync(Guid userId)
    {
        var notifications = await Repository.GetByRecipientIdAsync(userId);
        return Mapper.Map<List<GetNotificationDto>>(notifications);
    }

    public async Task MarkAsReadAsync(Guid notificationId)
    {
        var notification = await Repository.GetByIdAsync(notificationId);
        if (notification == null)
            throw new Exception("Bildirishnoma topilmadi");

        notification.IsRead = true;
        await Repository.UpdateAsync(notification);

    }
}