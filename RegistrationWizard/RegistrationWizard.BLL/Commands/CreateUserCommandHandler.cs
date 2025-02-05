using MediatR;
using RegistrationWizard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command handler for creating a new user.
/// </summary>
public class CreateUserCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<CreateUserCommand, UserRequestDTO>
{
    public async Task<UserRequestDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            CountryId = request.CountryId,
            ProvinceId = request.ProvinceId
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        return new UserRequestDTO();
    }
}