using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Users;
using ProductStore.Service.Interfaces.Users;
using System.Threading.Tasks;

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
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertAsync([FromBody] UserForCreationDto dto)
        {
            var result = await _userService.CreateAsync(dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await _userService.RetrieveAllAsync(@params);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _userService.RetrieveByIdAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var result = await _userService.RemoveAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserForUpdateDto dto)
        {
            var result = await _userService.ModifyAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpPut("change-password")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<UserForResultDto>> ChangePasswordAsync(long id, [FromBody] UserForChangePasswordDto dto)
        {
            var result = await _userService.ChangePasswordAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
