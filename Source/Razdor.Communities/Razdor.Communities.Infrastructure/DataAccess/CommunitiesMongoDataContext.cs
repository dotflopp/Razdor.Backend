using Microsoft.EntityFrameworkCore;
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new CommunityConfiguration())
            .ApplyConfiguration(new RolesConfiguratoin())
            .ApplyConfiguration(new CommunityUserConfiguration())
            .ApplyConfiguration(new VoiceChannelConfiguration())
            .ApplyConfiguration(new MessageChannelConfiguration())
            .ApplyConfiguration(new ForkChannelConfiguration())
            .ApplyConfiguration(new CategoryChannelConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}