using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using RegistrationWizard.BLL.Commands;
using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userDto">User object containing registration data.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// <para><b>200 OK</b> - Registration successful.</para>
    /// <para><b>400 Bad Request</b> - Invalid input data.</para>
    /// <para><b>500 Internal Server Error</b> - Server error occurred.</para>
    /// </returns>
    [HttpPost("")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Creates a new user record if the provided data is valid."
    )]
    [SwaggerResponse(200, "Success", typeof(RegisterPostResponseDTO))]
    [SwaggerResponse(400, "BadRequest", typeof(RegisterPostResponseDTO))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> Register([FromBody] UserRequestDto userDto, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand
        {
            Email = userDto.Email,
            Password = userDto.Password,
            CountryId = userDto.CountryId,
            ProvinceId = userDto.ProvinceId
        };
        _ = await mediator.Send(command, cancellationToken);
        return Ok(new RegisterPostResponseDTO { Message = "User registered successfully", Success = true });
    }
}

