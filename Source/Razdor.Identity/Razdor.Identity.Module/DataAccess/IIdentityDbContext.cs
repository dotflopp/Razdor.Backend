using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.DataAccess;

public interface IIdentityDbContext: IUnitOfWork
{
    public DbSet<UserAccount> UserAccounts { get; }
}