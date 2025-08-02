using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
    }
}
