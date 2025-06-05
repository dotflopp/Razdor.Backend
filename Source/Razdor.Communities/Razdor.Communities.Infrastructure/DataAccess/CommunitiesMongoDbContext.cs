using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;
using Razdor.Communities.Infrastructure.DataAccess.TypeConverters;
using Razdor.Communities.Module.DataAccess;

namespace Razdor.Communities.Infrastructure.DataAccess;

[method:RequiresUnreferencedCode("EF Core isn't fully compatible with trimming, and running the application may generate unexpected runtime failures. Some specific coding pattern are usually required to make trimming work properly, see https://aka.ms/efcore-docs-trimming for more details.")]
[method:RequiresDynamicCode("EF Core isn't fully compatible with NativeAOT, and running the application may generate unexpected runtime failures.")]
public class CommunitiesMongoDbContext(
    DbContextOptions<CommunitiesMongoDbContext> options
) : CommunitiesDbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTimeOffset>()
            .HaveConversion<DatetimeOffsetConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration<TextChannel>(new ChannelConfigurations())
            .ApplyConfiguration<CommunityChannel>(new ChannelConfigurations())
            .ApplyConfiguration<OverwritesPermissionChannel>(new ChannelConfigurations())
            .ApplyConfiguration<ForkChannel>(new ChannelConfigurations())
            .ApplyConfiguration<VoiceChannel>(new ChannelConfigurations())
            .ApplyConfiguration<CategoryChannel>(new ChannelConfigurations())
            .ApplyConfiguration(new CommunityConfiguration())
            .ApplyConfiguration(new CommunityMemberConfiguration())
            .ApplyConfiguration(new InvitesConfiguration());
    }
}