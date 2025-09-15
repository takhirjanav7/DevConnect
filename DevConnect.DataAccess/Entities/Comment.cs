﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public int LikesCount { get; set; }
    }
}
