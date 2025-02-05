using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.BLL.DTOs;
using AutoMapper;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query handler to retrieve all countries.
/// </summary>
public class GetAllCountriesQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryResponseDTO>>
{
    public async Task<IEnumerable<CountryResponseDTO>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await context.Countries
            .Include(c => c.Provinces)
            .ToListAsync(cancellationToken);


        var resultDto = mapper.Map<List<CountryResponseDTO>>(countries);
        return resultDto;
    }
}
