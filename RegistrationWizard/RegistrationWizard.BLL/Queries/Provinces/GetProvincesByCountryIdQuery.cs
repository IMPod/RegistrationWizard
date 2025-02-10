using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQuery(int countryId) : IRequest<BaseResponseDto<ProvinceResponceDTO>>
{
    public int CountryId { get; } = countryId;
}
