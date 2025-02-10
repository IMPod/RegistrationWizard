using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command for creating a new user.
/// </summary>
public class CreateUserCommand : IRequest<UserResponseDto>
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
    public int CountryId { get; init; }
    public int ProvinceId { get; init; }
}
