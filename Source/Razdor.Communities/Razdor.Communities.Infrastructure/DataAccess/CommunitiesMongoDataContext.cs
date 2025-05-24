using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations;
using Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;
using Razdor.Communities.Services;
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
        
        configurationBuilder.Properties(typeof(DateTimeOffset), builder =>
        {
            builder.HaveConversion<DatetimeOffsetConverter>();
        });
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
            .ApplyConfiguration(new CommunityUserConfiguration());
    }
}