using MediatR;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RegistrationWizard.BLL.DTOs;

namespace RegistrationWizard.BLL.Queries.Countries;

/// <summary>
/// Query handler to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQueryHandler(RegistrationContext context, IMapper mapper) : IRequestHandler<GetCountryByIdQuery, CountryResponseDTO?>
{
    public async Task<CountryResponseDTO?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var resultDto = await context.Countries
            .AsNoTracking() 
            .Where(c => c.Id == request.CountryId)
            .ProjectTo<CountryResponseDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return resultDto;
    }
}
