using System.Diagnostics.CodeAnalysis;
using Mediator;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.Sql.EntityConfigurations;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Infrastructure;
using Razdor.Shared.Module.DbExceptions;

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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try 
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception) when (exception.InnerException is SqliteException { SqliteErrorCode: 19} sqliteException)
        {
            throw new UniqueConstraintException(sqliteException.Message, sqliteException);
        }
    }

    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateException exception) when (exception.InnerException is SqliteException { SqliteErrorCode: 19} sqliteException)
        {
            throw new UniqueConstraintException(sqliteException.Message, sqliteException);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}