using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query handler to retrieve a country by its identifier.
/// </summary>
public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Country?>
{
    private readonly RegistrationContext _context;

    public GetCountryByIdQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<Country?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Countries
            .Include(c => c.Provinces)
            .FirstOrDefaultAsync(c => c.Id == request.CountryId, cancellationToken);
    }
}
