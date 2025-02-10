using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using RegistrationWizard.BLL.Queries.Countries;
using RegistrationWizard.BLL.Queries.Provinces;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.Server.Controllers;

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
    [SwaggerResponse(200, "Success", typeof(BaseResponseDto<CountryResponseDTO>))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllCountriesQuery(), cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves the list of provinces for a specified country.
    /// </summary>
    /// <param name="countryId">The identifier of the country to retrieve provinces for.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// <para><b>200 OK</b> - A list of provinces for the specified country.</para>
    /// <para><b>500 Internal Server Error</b> - If a server error occurs.</para>
    /// </returns>
    [HttpGet("{countryId}/provinces")]
    [SwaggerOperation(
        Summary = "Retrieve provinces by country identifier",
        Description = "Returns a list of provinces for the country specified by its identifier."
    )]
    [SwaggerResponse(200, "Success", typeof(BaseResponseDto<ProvinceResponceDTO>))]
    [SwaggerResponse(500, "Server error", typeof(string))]
    public async Task<IActionResult> GetProvincesByCountry(int countryId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetProvincesByCountryIdQuery(countryId), cancellationToken);
        return Ok(response);
    }
}
