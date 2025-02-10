using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Provinces;

/// <summary>
/// Query handler to retrieve a province by its identifier.
/// </summary>
public class GetProvinceByIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetProvinceByIdQuery, ProvinceResponceDTO?>
{
    public async Task<ProvinceResponceDTO?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var resultDto = await context.Provinces
            .AsNoTracking()
            .Where(c => c.Id == request.ProvinceId)
            .ProjectTo<ProvinceResponceDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return resultDto;
    }
}
