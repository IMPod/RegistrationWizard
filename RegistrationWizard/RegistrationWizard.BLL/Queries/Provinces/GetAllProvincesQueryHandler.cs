using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;
using AutoMapper.QueryableExtensions;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query handler to retrieve all provinces.
/// </summary>
public class GetAllProvincesQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetAllProvincesQuery, BaseResponseDto<ProvinceResponceDTO>>
{
    public async Task<BaseResponseDto<ProvinceResponceDTO>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        var resultDto = await context.Provinces
            .AsNoTracking()
            .ProjectTo<ProvinceResponceDTO>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new BaseResponseDto<ProvinceResponceDTO>()
        {
            Data = resultDto.ToList()
        };
        return response;
    }
}
