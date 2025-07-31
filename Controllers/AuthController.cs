using DevConnect.BLL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly object _tokenLogRepo;

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
        {
            await _tokenLogRepo.InvalidateTokenAsync(dto.RefreshToken);
            return Ok(new { message = "Logged out successfully" });
        }

    }
}
