using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Infrastructure.DataAccess.EntityConfigurations;
using Razdor.Identity.Module.DataAccess;

namespace Razdor.Identity.Infrastructure.DataAccess;

public class IdentityPostgresDbContext(
    DbContextOptions<IdentityPostgresDbContext> options
) : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}