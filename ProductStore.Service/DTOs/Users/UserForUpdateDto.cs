using ProductStore.Domain.Enums;

namespace ProductStore.Service.DTOs.Users;

public record UserForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole Role { get; set; }
}
