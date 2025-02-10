using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Countries;

/// <summary>
/// Query to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQuery : IRequest<CountryResponseDTO?>
{
    public int CountryId { get; set; }

    public GetCountryByIdQuery(int countryId)
    {
        CountryId = countryId;
    }
}
