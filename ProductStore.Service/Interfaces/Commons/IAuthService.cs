using ProductStore.Domain.Entities.Users;

namespace ProductStore.Service.Interfaces.Commons;

public interface IAuthService
{
    public string GenerateToken(User users);
}