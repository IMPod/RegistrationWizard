using MediatR;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query to retrieve a province by its identifier.
/// </summary>
public class GetProvinceByIdQuery : IRequest<Province?>
{
    public int ProvinceId { get; set; }

    public GetProvinceByIdQuery(int provinceId)
    {
        ProvinceId = provinceId;
    }
}
