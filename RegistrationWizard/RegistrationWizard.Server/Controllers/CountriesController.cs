using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using RegistrationWizard.BLL.Queryes.Countries;
using RegistrationWizard.BLL.Queryes.Provinces;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IMediator mediator) : ControllerBase
{
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
        var result = await mediator.Send(new GetAllCountriesQuery());

        var responce = new BaseResponseDTO<CountryResponseDTO>()
        {
            Data = result.ToList()
        };
        return Ok(responce);
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
        var provinces = await mediator.Send(new GetProvincesByCountryIdQuery(countryId));

        var response = new BaseResponseDTO<ProvinceResponceDTO>()
        {
            Data = provinces.ToList()
        };
        return Ok(response);
    }
}
