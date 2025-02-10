using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queries.Users;

/// <summary>
/// Query to retrieve a user by its identifier.
/// </summary>
public class GetUserByIdQuery : IRequest<AppUser?>
{
    public int UserId { get; set; }

    public GetUserByIdQuery(int userId)
    {
        UserId = userId;
    }
}
