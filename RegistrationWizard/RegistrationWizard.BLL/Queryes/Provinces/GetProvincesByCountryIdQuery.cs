using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQuery : IRequest<IEnumerable<Province>>
{
    public int CountryId { get; }

    public GetProvincesByCountryIdQuery(int countryId)
    {
        CountryId = countryId;
    }
}
