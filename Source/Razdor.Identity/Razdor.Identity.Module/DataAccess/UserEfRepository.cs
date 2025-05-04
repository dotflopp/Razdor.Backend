using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.DataAccess;

public class UserEfRepository(IdentityDbSqlContext dbSqlContext) : IUserRepository
{
    public IUnitOfWork UnitOfWork => dbSqlContext;

    public UserAccount Add(UserAccount user)
    {
        var entry = dbSqlContext.UserAccounts.Add(user);
        return entry.Entity;
    }

    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        var user = await dbSqlContext.UserAccounts
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<UserAccount?> FindByIdAsync(ulong id)
    {
        var user = await dbSqlContext.UserAccounts
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        return user;
    }
}