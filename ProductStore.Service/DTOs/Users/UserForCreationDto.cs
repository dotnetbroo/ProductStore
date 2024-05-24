using System.ComponentModel.DataAnnotations;

namespace ProductStore.Service.DTOs.Users;

public record UserForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required(ErrorMessage = "Username is required")]
    [MinLength(5), MaxLength(32)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8), MaxLength(32)]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
