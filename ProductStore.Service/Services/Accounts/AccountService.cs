using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Domain.Entities.Users;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Helpers;
using ProductStore.Service.DTOs.Logins;
using ProductStore.Service.Interfaces.Accounts;
using ProductStore.Service.Interfaces.Commons;

namespace ProductStore.Service.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAuthService authService;
    private readonly IRepository<User> userRepository;

    public AccountService(IRepository<User> userRepository, IAuthService authService)
    {
        this.authService = authService;
        this.userRepository = userRepository;
    }
    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await this.userRepository.SelectAll()
            .Where(x => x.PhoneNumber == loginDto.PhoneNumber)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CustomException(404, "Telefor raqam yoki parol xato kiritildi!");

        var hasherResult = PasswordHelper.Verify(loginDto.Password, user.Salt, user.Password);
        if (hasherResult == false)
            throw new CustomException(404, "Telefor raqam yoki parol xato kiritildi!");

        return authService.GenerateToken(user);
    }
}
