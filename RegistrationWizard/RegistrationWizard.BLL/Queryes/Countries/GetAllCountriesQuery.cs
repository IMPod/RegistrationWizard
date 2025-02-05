using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query to retrieve all countries.
/// </summary>
public class GetAllCountriesQuery : IRequest<IEnumerable<CountryResponseDTO>>
{
}
