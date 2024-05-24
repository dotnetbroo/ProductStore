using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Logins;
using ProductStore.Service.DTOs.Users;

namespace ProductStore.Service.Interfaces.Users;

public interface IUserService
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
