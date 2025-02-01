using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query handler to retrieve all provinces.
/// </summary>
public class GetAllProvincesQueryHandler : IRequestHandler<GetAllProvincesQuery, IEnumerable<Province>>
{
    private readonly RegistrationContext _context;

    public GetAllProvincesQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Province>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Provinces.ToListAsync(cancellationToken);
    }
}
