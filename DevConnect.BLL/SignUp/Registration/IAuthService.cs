using DevConnect.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.SignUp.Registration;

public interface IAuthService
{
    Task RegisterAsync(RegisterUserDto dto);
    Task<LogOutResultDto> LoginAsync(LoginUserDto dto);
    Task<TokenDto> RefreshTokenAsync(string refreshToken);
}
