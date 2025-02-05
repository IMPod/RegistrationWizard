using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query handler to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetCountryByIdQuery, List<CountryResponseDTO>?>
{
    public async Task<List<CountryResponseDTO>?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var countries = await context.Countries
            .Include(c => c.Provinces)
            .FirstOrDefaultAsync(c => c.Id == request.CountryId, cancellationToken);

        var resultDto = mapper.Map<List<CountryResponseDTO>>(countries).ToList();

        return resultDto;
    }
}
