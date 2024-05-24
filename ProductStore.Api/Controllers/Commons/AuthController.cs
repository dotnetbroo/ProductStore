using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Helpers;
using ProductStore.Service.DTOs.Logins;
using ProductStore.Service.Interfaces.Accounts;
using System.Threading.Tasks;

namespace ProductStore.Api.Controllers.Commons
{
    public class AuthController : BaseController
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async ValueTask<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
