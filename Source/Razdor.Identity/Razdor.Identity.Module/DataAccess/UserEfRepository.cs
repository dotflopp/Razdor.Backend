using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Identity.Module.DataAccess;

public class UserEfRepository(IdentityDbContext context, UnitOfWork<IdentityDbContext> unitOfWork) : IUserRepository
{
    public IUnitOfWork UnitOfWork => unitOfWork;

    public UserAccount Add(UserAccount user)
    {
        EntityEntry<UserAccount> entry = context.UserAccounts.Add(user);
        return entry.Entity;
    }

    public async Task<UserAccount> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        UserAccount? user = await context.UserAccounts
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        ResourceNotFoundException.ThrowIfNull(user);
        
        return user;
    }

    public async Task<UserAccount> FindByIdAsync(ulong id, CancellationToken cancellationToken = default)
    {
        UserAccount? user = await context.UserAccounts
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        
        ResourceNotFoundException.ThrowIfNull(user);
        
        return user;
    }
}