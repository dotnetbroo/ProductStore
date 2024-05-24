using ProductStore.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Service.DTOs.Logins;

public record LoginDto
{
    [Required(ErrorMessage = "Telefon raqamni kiriting"), PhoneNumber]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Parolni kiriting"), StrongPassword]
    public string Password { get; set; }
}
