using MediatR;
using RegistrationWizard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using RegistrationWizard.BLL.DTOs;
using AutoMapper;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command handler for creating a new user.
/// </summary>
public class CreateUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<CreateUserCommand, UserRequestDto>
{
    public async Task<UserRequestDto> Handle(CreateUserCommand responce, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = responce.Email,
            Email = responce.Email,
            CountryId = responce.CountryId,
            ProvinceId = responce.ProvinceId
        };

        var result = await userManager.CreateAsync(user, responce.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }
        var request = mapper.Map<UserRequestDto>(user);
        return request;
    }
}