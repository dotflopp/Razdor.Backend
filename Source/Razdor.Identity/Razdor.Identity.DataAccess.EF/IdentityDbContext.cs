using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.DataAccess.EntityConfigurations;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Infrastructure;

namespace Razdor.Identity.DataAccess;

public class IdentityDbContext(
    IMediator mediator,
    DbContextOptions<IdentityDbContext> options
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