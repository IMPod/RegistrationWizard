using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using RegistrationWizard.BLL.Commands;
using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userDto">User object containing registration data.</param>
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
    public async Task<IActionResult> Register([FromBody] UserRequestDTO userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Email) ||
            string.IsNullOrWhiteSpace(userDto.Password) ||
            userDto.CountryId <= 0 ||
            userDto.ProvinceId <= 0)
        {
            return BadRequest(new RegisterPostResponseDTO { Errors = "Invalid data.", IsError = true });
        }

        var command = new CreateUserCommand
        {
            Email = userDto.Email,
            Password = userDto.Password,
            CountryId = userDto.CountryId,
            ProvinceId = userDto.ProvinceId
        };

        var createdUser = await mediator.Send(command);
        return Ok(new RegisterPostResponseDTO { Message = "User registered successfully", Success = true });
    }
}

