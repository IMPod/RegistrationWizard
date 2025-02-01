using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Provinces;

/// <summary>
/// Query handler to retrieve a province by its identifier.
/// </summary>
public class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceByIdQuery, Province?>
{
    private readonly RegistrationContext _context;

    public GetProvinceByIdQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<Province?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Id == request.ProvinceId, cancellationToken);
    }
}
