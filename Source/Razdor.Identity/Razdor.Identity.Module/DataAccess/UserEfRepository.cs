using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Identity.Module.DataAccess;

public class UserEfRepository(IdentityDbContext context, IMediator mediator) : IUserRepository
{
    public IUnitOfWork UnitOfWork { get; } = new UnitOfWork(context, mediator);

    public UserAccount Add(UserAccount user)
    {
        var entry = context.UserAccounts.Add(user);
        return entry.Entity;
    }

    public async Task<UserAccount?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await context.UserAccounts
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<UserAccount?> FindByIdAsync(ulong id, CancellationToken cancellationToken = default)
    {
        var user = await context.UserAccounts
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }
}