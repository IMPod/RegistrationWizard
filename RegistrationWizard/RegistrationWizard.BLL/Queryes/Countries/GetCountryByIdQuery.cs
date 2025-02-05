using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQuery : IRequest<List<CountryResponseDTO>?>
{
    public int CountryId { get; set; }

    public GetCountryByIdQuery(int countryId)
    {
        CountryId = countryId;
    }
}
