using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query handler to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetProvincesByCountryIdQuery, BaseResponseDto<ProvinceResponceDTO>>
{
    public async Task<BaseResponseDto<ProvinceResponceDTO>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var resultDto = await context.Provinces
            .AsNoTracking()
            .Where(p => p.CountryId == request.CountryId)
            .ProjectTo<ProvinceResponceDTO>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new BaseResponseDto<ProvinceResponceDTO>()
        {
            Data = resultDto.ToList()
        };
        return response;
    }
}