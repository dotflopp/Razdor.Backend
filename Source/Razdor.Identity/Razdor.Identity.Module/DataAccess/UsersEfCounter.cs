using Microsoft.EntityFrameworkCore;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain;

namespace Razdor.Identity.Module.DataAccess;

public class UsersEfCounter(IIdentityDbContext context): IUsersCounter
{
    public Task<int> CountUserWithEmailAsync(string email)
    {
        return context.UserAccounts
            .Where(x => x.Email == email)
            .CountAsync();
    }

    public Task<int> CountUserWithIdentityName(string identityName)
    {
        return context.UserAccounts
            .Where(x => x.IdentityName == identityName)
            .CountAsync();
    }
}