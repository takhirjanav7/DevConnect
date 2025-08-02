using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Level {  get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User Users { get; set; }
    }
}
