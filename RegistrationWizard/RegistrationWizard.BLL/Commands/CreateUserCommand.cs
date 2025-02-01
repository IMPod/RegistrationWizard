using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command for creating a new user.
/// </summary>
public class CreateUserCommand : IRequest<User>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}
