using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductStore.Data.IRepositories;
using ProductStore.Data.Repositories;
using ProductStore.Domain.Configurations;
using ProductStore.Domain.Entities.Users;
using ProductStore.Domain.Enums;
using ProductStore.Service.Commons.Exceptions;
using ProductStore.Service.Commons.Extensions;
using ProductStore.Service.Commons.Helpers;
using ProductStore.Service.DTOs.Logins;
using ProductStore.Service.DTOs.Users;
using ProductStore.Service.Interfaces.Commons;
using ProductStore.Service.Interfaces.Users;

namespace ProductStore.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;
    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Username.ToLower() == dto.Username.ToLower() && u.PhoneNumber == dto.PhoneNumber)
            .FirstOrDefaultAsync();
        if (user is not null)
            throw new CustomException(403, "User is already exsit.");

        var hasherResult = PasswordHelper.Hash(dto.Password);
        var mapped = _mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Salt = hasherResult.Salt;
        mapped.Password = hasherResult.Hash;
        mapped.Role = UserRole.Customer;

        var result = await _userRepository.InsertAsync(mapped);
        return _mapper.Map<UserForResultDto>(result);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found");

        var mapped = _mapper.Map(dto, user);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(mapped);

        return _mapper.Map<UserForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _userRepository.SelectAll()
              .Where(u => u.Id == id)
              .AsNoTracking()
              .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "User is not found");

        await _userRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.SelectAll()
             .AsNoTracking()
             .ToPagedList(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var user = await _userRepository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new CustomException(404, "Foydalanuvchi topilmadi.");

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (user is null || !PasswordHelper.Verify(dto.OldPassword, user.Salt, user.Password))
            throw new CustomException(404, "Eski parol xato!");
        else if (dto.NewPassword != dto.ConfirmPassword)
            throw new CustomException(400, "Yangi parol va tasdiqlash paroli bir xil emas!\nXatolikni to'g'rilang!");

        var hash = PasswordHelper.Hash(dto.ConfirmPassword);
        user.Salt = hash.Salt;
        user.Password = hash.Hash;

        await _userRepository.UpdateAsync(user);

        return true;
    }

}