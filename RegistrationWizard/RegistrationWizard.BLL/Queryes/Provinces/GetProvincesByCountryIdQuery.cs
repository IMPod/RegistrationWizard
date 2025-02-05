using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQuery : IRequest<IEnumerable<ProvinceResponceDTO>>
{
    public int CountryId { get; }

    public GetProvincesByCountryIdQuery(int countryId)
    {
        CountryId = countryId;
    }
}
