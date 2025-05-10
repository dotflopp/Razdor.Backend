using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.Module.DataAccess;

public interface IIdentityDbContext: IUnitOfWork
{
    DbSet<UserAccount> UserAccounts { get; }
}