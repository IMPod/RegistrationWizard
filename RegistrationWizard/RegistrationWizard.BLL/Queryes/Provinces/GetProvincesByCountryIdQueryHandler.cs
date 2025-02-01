using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query handler to retrieve provinces by country identifier.
/// </summary>
public class GetProvincesByCountryIdQueryHandler : IRequestHandler<GetProvincesByCountryIdQuery, IEnumerable<Province>>
{
    private readonly RegistrationContext _context;

    public GetProvincesByCountryIdQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Province>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Provinces
            .Where(p => p.CountryId == request.CountryId)
            .ToListAsync(cancellationToken);
    }
}
