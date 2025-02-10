using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query handler to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetProvincesByCountryIdQuery, BaseResponseDto<ProvinceResponceDTO>>
{
    public async Task<BaseResponseDto<ProvinceResponceDTO>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var provinces = await context.Provinces
            .Where(p => p.CountryId == request.CountryId)
            .ToListAsync(cancellationToken);

        var resultDto = mapper.Map<List<ProvinceResponceDTO>>(provinces);
        var response = new BaseResponseDto<ProvinceResponceDTO>()
        {
            Data = resultDto.ToList()
        };
        return response;
    }
}
