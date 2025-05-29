using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.DataAccess;

[method:RequiresUnreferencedCode("EF Core isn't fully compatible with trimming, and running the application may generate unexpected runtime failures. Some specific coding pattern are usually required to make trimming work properly, see https://aka.ms/efcore-docs-trimming for more details.")]
[method:RequiresDynamicCode("EF Core isn't fully compatible with NativeAOT, and running the application may generate unexpected runtime failures.")]
public abstract class IdentityDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts { get; protected set; }
}