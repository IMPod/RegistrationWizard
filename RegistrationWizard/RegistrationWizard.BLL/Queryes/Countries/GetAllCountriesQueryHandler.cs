using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Countries;

/// <summary>
/// Query handler to retrieve all countries.
/// </summary>
public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<Country>>
{
    private readonly RegistrationContext _context;

    public GetAllCountriesQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Countries
            .Include(c => c.Provinces)
            .ToListAsync(cancellationToken);
    }
}
