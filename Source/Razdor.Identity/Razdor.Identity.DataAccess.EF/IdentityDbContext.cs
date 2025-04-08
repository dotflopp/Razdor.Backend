using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain;
using Razdor.Shared.Domain;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Infrastructure;

namespace Razdro.Identity.DataAccess;

public class IdentityDbContext(
    IMediator mediator,
    DbContextOptions<IdentityDbContext> options
): DbContext(options), IUnitOfWork
{
    public DbSet<UserAccount> UserAccounts { get; private set; }
    
    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        int writtenCount = await base.SaveChangesAsync(cancellationToken);
        await mediator.DispatchDomainEventsAsync<BaseEntity>(this);
        
        return writtenCount;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}
