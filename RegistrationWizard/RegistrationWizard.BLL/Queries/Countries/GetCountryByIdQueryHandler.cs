using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Countries;

/// <summary>
/// Query handler to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetCountryByIdQuery, CountryResponseDTO?>
{
    public async Task<CountryResponseDTO?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await context.Countries
            .Include(c => c.Provinces)
            .FirstOrDefaultAsync(c => c.Id == request.CountryId, cancellationToken);

        var resultDto = mapper.Map<CountryResponseDTO>(country);

        return resultDto;
    }
}
