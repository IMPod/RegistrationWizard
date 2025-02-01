using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL;
using Swashbuckle.AspNetCore.Annotations;
using RegistrationWizard.DTOs;

namespace RegistrationWizard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly RegistrationContext _context;

    public CountriesController(RegistrationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves the list of countries along with their provinces.
    /// </summary>
    /// <returns>
    /// <para><b>200 OK</b> - A list of countries with their associated provinces.</para>
    /// <para><b>500 Internal Server Error</b> - If a server error occurs.</para>
    /// </returns>
    [HttpGet("")]
    [SwaggerOperation(
        Summary = "Retrieve list of countries with provinces",
        Description = "Returns all countries, each containing a list of its provinces."
    )]
    [SwaggerResponse(200, "Success", typeof(BaseResponseDTO<CountryResponseDTO>))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await _context.Countries.ToListAsync();
            var result = countries.Select(x => new CountryResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Provinces = x.Provinces.Select(p=> new ProvinceResponceDTO()
                {
                    Id=p.Id,
                    Name = p.Name,
                    CountryId = p.CountryId,
                }).ToList(),
            }).ToList();

            var responce = new BaseResponseDTO<CountryResponseDTO>()
            {
                Data = result
            };
            return Ok(responce);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorDTO(ex));
        }
    }

    /// <summary>
    /// Retrieves the list of provinces for a specified country.
    /// </summary>
    /// <param name="countryId">The identifier of the country to retrieve provinces for.</param>
    /// <returns>
    /// <para><b>200 OK</b> - A list of provinces for the specified country.</para>
    /// <para><b>500 Internal Server Error</b> - If a server error occurs.</para>
    /// </returns>
    [HttpGet("{countryId}/provinces")]
    [SwaggerOperation(
        Summary = "Retrieve provinces by country identifier",
        Description = "Returns a list of provinces for the country specified by its identifier."
    )]
    [SwaggerResponse(200, "Success", typeof(BaseResponseDTO<ProvinceResponceDTO>))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> GetProvincesByCountry(int countryId)
    {
        try
        {
            var provinces = await _context.Provinces
                .Where(p => p.CountryId == countryId)
                .ToListAsync();

            var result = provinces.Select(p => new ProvinceResponceDTO
            {
                Id = p.Id,
                Name = p.Name,
                CountryId = p.CountryId,
            }).ToList();

            var response = new BaseResponseDTO<ProvinceResponceDTO>()
            {
                Data = result
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorDTO(ex));
        }
    }
}
