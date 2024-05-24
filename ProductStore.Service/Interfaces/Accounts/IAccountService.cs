using ProductStore.Service.DTOs.Logins;

namespace ProductStore.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<string> LoginAsync(LoginDto loginDto);
    }
}
