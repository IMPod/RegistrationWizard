using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query handler to retrieve all provinces.
/// </summary>
public class GetAllProvincesQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetAllProvincesQuery, IEnumerable<ProvinceResponceDTO>>
{
    public async Task<IEnumerable<ProvinceResponceDTO>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {

        var provinces = await context.Provinces
           .ToListAsync(cancellationToken);

        var resultDto = mapper.Map<List<ProvinceResponceDTO>>(provinces);
        return resultDto;
    }
}
