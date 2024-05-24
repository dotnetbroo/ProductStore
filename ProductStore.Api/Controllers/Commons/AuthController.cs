using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Helpers;
using ProductStore.Service.DTOs.Logins;
using ProductStore.Service.Interfaces.Accounts;
using ProductStore.Service.Interfaces.Commons;

namespace ProductStore.Api.Controllers.Commons;

public class AuthController : BaseController
{
    private readonly IAccountService accountService;

    public AuthController(IAccountService accountService, IAuthService authService)
    {
        this.accountService = accountService;
    }

    [HttpPost]
    [Route("login")]
    public async ValueTask<IActionResult> login([FromBody] LoginDto loginDto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = await accountService.LoginAsync(loginDto)
        });
}
