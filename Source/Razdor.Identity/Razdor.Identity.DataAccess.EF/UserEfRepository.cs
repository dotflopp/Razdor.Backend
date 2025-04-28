using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.DataAccess;

public class UserEfRepository(IdentityDbContext dbContext) : IUserRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
    public UserAccount Add(UserAccount user)
    {
        EntityEntry<UserAccount> entry = dbContext.UserAccounts.Add(user);
        return entry.Entity;
    }

    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        UserAccount? user = await dbContext.UserAccounts
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();

        return user;
    }
}