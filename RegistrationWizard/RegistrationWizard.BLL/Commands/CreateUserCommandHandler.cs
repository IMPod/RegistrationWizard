using MediatR;
using RegistrationWizard.DAL.Models;
using Microsoft.AspNetCore.Identity;
using RegistrationWizard.BLL.DTOs;
using AutoMapper;

namespace RegistrationWizard.BLL.Commands;

/// <summary>
/// Command handler for creating a new user.
/// </summary>
public class CreateUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<AppUser>(request);

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        var responseDto = mapper.Map<UserResponseDto>(user);
        return responseDto;
    }
}