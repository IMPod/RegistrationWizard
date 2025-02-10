using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Countries;

/// <summary>
/// Query to retrieve all countries.
/// </summary>
public class GetAllCountriesQuery : IRequest<BaseResponseDto<CountryResponseDTO>>
{
}
