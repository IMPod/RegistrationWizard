namespace RegistrationWizard.BLL.DTOs;

public class UserRequestDto
{
    public int Id { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required int CountryId { get; init; }
    public required int ProvinceId { get; init; }
}
