using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.PostgreSQL.EntityConfigurations;
using Razdor.Identity.Module.DataAccess;

namespace Razdor.Identity.Infrastructure.DataAccess.PostgreSQL;

public class IdentityPostgreSqlContext(
    DbContextOptions<IdentityPostgreSqlContext> options
) : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}