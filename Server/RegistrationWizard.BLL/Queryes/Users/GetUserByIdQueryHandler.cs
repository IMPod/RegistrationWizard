﻿using MediatR;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.DAL;
using Microsoft.EntityFrameworkCore;

namespace RegistrationWizard.BLL.Queryes.Users;

/// <summary>
/// Query handler to retrieve a user by its identifier.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly RegistrationContext _context;

    public GetUserByIdQueryHandler(RegistrationContext context)
    {
        _context = context;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
    }
}
