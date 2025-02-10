using System.ComponentModel.DataAnnotations;

namespace RegistrationWizard.BLL.DTOs;

public class UserRequestDto
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "CountryId must be > 0")]
    public int CountryId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "ProvinceId must be > 0")]
    public int ProvinceId { get; init; }
}
