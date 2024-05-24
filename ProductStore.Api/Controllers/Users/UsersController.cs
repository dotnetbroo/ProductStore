using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Users;
using ProductStore.Service.Interfaces.Users;

namespace ProductStore.Api.Controllers.Users
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromForm] UserForCreationDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _userService.CreateAsync(dto)
            });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _userService.RetrieveByIdAsync(id)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _userService.RemoveAsync(id)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] UserForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _userService.ModifyAsync(id, dto)
            });

        [HttpPut("change-password")]
        public async Task<ActionResult<UserForResultDto>> ChangePasswordAsync(long id, [FromForm] UserForChangePasswordDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _userService.ChangePasswordAsync(id, dto)
            });
    }
}
