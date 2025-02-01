using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command handler for creating a new user.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly RegistrationContext _context;

    public CreateUserCommandHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            CountryId = request.CountryId,
            ProvinceId = request.ProvinceId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }
}