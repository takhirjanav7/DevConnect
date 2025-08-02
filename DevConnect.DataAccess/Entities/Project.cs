using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string? RepositoryUrl {  get; set; } 
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public bool TeamMembers { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }
}
