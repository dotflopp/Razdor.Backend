using System.Diagnostics.CodeAnalysis;
using Mediator;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.Sql.EntityConfigurations;
using Razdor.Identity.Module.DataAccess;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Infrastructure;

namespace Razdor.Identity.Infrastructure.DataAccess.Sql;

public class IdentitySqlliteDbContext(
    IMediator mediator,
    DbContextOptions<IdentitySqlliteDbContext> options
) : DbContext(options), IIdentityDbContext, IUnitOfWork
{
    public DbSet<UserAccount> UserAccounts { get; private set; }

    /// <inheritdoc />
    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var writtenCount = await SaveChangesAsync(cancellationToken);
        await mediator.DispatchDomainEventsAsync(this);

        return writtenCount;
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}