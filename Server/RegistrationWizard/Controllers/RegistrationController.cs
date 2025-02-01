using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Swashbuckle.AspNetCore.Annotations;
using RegistrationWizard.DTOs;

namespace RegistrationWizard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly RegistrationContext _context;

    public RegistrationController(RegistrationContext context)
    {
        _context = context;
    }


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
    [SwaggerResponse(200, "Success", typeof(RegisterPostRequestDTO))]
    [SwaggerResponse(400, "BadRequest", typeof(RegisterPostRequestDTO))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> Register([FromBody] User userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Email) ||
            string.IsNullOrWhiteSpace(userDto.Password) ||
            userDto.CountryId <= 0 ||
            userDto.ProvinceId <= 0)
        {
            return BadRequest(new RegisterPostRequestDTO { Errors = "Invalid data.", IsError = true });
        }

        try
        {
            _context.Users.Add(userDto);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorDTO(ex));
        }

        return Ok(new RegisterPostRequestDTO { Message = "User registered successfully", Success = true });
    }
}

