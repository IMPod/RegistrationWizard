using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.BLL.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace RegistrationWizard.BLL.Queries.Countries;

/// <summary>
/// Query handler to retrieve all countries.
/// </summary>
public class GetAllCountriesQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetAllCountriesQuery, BaseResponseDto<CountryResponseDTO>>
{
    public async Task<BaseResponseDto<CountryResponseDTO>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var resultDto = await context.Countries
            .AsNoTracking()
            .ProjectTo<CountryResponseDTO>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new BaseResponseDto<CountryResponseDTO>()
        {
            Data = resultDto.ToList()
        };
        return response;
    }
}
