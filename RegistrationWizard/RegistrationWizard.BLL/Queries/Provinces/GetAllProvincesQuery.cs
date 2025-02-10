using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query to retrieve all provinces.
/// </summary>
public class GetAllProvincesQuery : IRequest<BaseResponseDto<ProvinceResponceDTO>>
{
}