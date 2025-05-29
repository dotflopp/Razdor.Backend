using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;
using Razdor.Communities.Infrastructure.DataAccess.TypeConverters;
using Razdor.Communities.Services.DataAccess;

namespace Razdor.Communities.Infrastructure.DataAccess;

public class CommunityMongoDataContext(DbContextOptions options) : CommunityDataContext(options)
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