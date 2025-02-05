using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query handler to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetProvincesByCountryIdQuery, IEnumerable<ProvinceResponceDTO>>
{
    public async Task<IEnumerable<ProvinceResponceDTO>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var provinces = await context.Provinces
            .Where(p => p.CountryId == request.CountryId)
            .ToListAsync(cancellationToken);

        var resultDto = mapper.Map<List<ProvinceResponceDTO>>(provinces);
        return resultDto;
    }
}
