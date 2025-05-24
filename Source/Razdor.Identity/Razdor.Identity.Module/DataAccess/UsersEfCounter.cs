using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.DataAccess;

public class UsersEfCounter(IdentityDbContext context): IUsersCounter
{
    public Task<int> CountUserWithIdentityNameOrEmail(string identityName, string email)
    {
        return context.UserAccounts
            .Where(x => x.IdentityName == identityName || x.Email == email)
            .CountAsync();
    }
}