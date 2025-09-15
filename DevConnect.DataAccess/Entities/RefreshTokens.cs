using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.DataAccess.Entities;

public class RefreshTokens
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
