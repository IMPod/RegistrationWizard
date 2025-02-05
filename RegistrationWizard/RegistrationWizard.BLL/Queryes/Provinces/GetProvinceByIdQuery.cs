using MediatR;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve a province by its identifier.
/// </summary>
public class GetProvinceByIdQuery : IRequest<ProvinceResponceDTO?>
{
    public int ProvinceId { get; set; }

    public GetProvinceByIdQuery(int provinceId)
    {
        ProvinceId = provinceId;
    }
}
