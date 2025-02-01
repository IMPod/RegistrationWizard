using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQuery : IRequest<Country?>
{
    public int CountryId { get; set; }

    public GetCountryByIdQuery(int countryId)
    {
        CountryId = countryId;
    }
}
