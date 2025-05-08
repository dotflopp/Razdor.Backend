using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.Module.DataAccess;

public class UserEfRepository(IIdentityDbContext context) : IUserRepository
{
    public IUnitOfWork UnitOfWork => context;

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