using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.DataAccess;

public class UsersEfCounter(IdentityDbContext context): IUsersCounter
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