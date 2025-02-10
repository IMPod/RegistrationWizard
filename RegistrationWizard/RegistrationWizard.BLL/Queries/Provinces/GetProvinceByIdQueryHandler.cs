using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query handler to retrieve a province by its identifier.
/// </summary>
public class GetProvinceByIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetProvinceByIdQuery, ProvinceResponceDTO?>
{
    public async Task<ProvinceResponceDTO?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var province = await context.Provinces
            .FirstOrDefaultAsync(p => p.Id == request.ProvinceId, cancellationToken);

        var resultDto = mapper.Map<ProvinceResponceDTO>(province);
        return resultDto;
    }
}
