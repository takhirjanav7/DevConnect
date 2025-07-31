using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.BLL.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService Service;

        public UserController(IUserService service)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
            => Ok(await Service.CreateAsync(dto));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await Service.GetAllAsync());

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
            => await Service.UpdateAsync(id, dto) ? Ok() 
                : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
            => await Service.DeleteAsync(id) ? Ok() : NotFound();
    }
}
