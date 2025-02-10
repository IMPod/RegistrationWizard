namespace RegistrationWizard.BLL.DTOs;

public class UserResponseDto
{
    public string Email { get; set; } = null!;
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}