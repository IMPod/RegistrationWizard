using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve all provinces.
/// </summary>
public class GetAllProvincesQuery : IRequest<IEnumerable<Province>>
{
}