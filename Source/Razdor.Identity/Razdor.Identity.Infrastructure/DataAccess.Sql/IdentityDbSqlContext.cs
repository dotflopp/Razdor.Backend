using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.Sql.EntityConfigurations;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.Infrastructure.DataAccess.Sql;

public class IdentityDbSqlContext(
    IMediator mediator,
    DbContextOptions<IdentityDbSqlContext> options
) : DbContext(options), IUnitOfWork
{
    public DbSet<UserAccount> UserAccounts { get; private set; }

    /// <inheritdoc />
    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var writtenCount = await base.SaveChangesAsync(cancellationToken);
        await mediator.DispatchDomainEventsAsync(this);

        return writtenCount;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}