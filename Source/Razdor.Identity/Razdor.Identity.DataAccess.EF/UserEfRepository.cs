using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.DataAccess;

public class UserEfRepository(IdentityDbContext dbContext) : IUserRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public UserAccount Add(UserAccount user)
    {
        var entry = dbContext.UserAccounts.Add(user);
        return entry.Entity;
    }

    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        var user = await dbContext.UserAccounts
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<UserAccount?> FindByIdAsync(ulong id)
    {
        var user = await dbContext.UserAccounts
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        return user;
    }
}