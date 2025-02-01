using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queryes.Users;

/// <summary>
/// Query to retrieve a user by its identifier.
/// </summary>
public class GetUserByIdQuery : IRequest<User?>
{
    public int UserId { get; set; }

    public GetUserByIdQuery(int userId)
    {
        UserId = userId;
    }
}
