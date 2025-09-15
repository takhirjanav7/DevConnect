﻿namespace DevConnect.Domain.Entities;

public class ProjectUser
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Role { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}