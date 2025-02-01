using RegistrationWizard.DAL.Models;
using MediatR;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query to retrieve all countries.
/// </summary>
public class GetAllCountriesQuery : IRequest<IEnumerable<Country>>
{
}
