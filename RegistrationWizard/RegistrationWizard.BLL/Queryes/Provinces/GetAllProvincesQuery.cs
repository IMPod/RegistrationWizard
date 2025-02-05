using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve all provinces.
/// </summary>
public class GetAllProvincesQuery : IRequest<IEnumerable<ProvinceResponceDTO>>
{
}