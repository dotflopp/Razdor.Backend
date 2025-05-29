using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Infrastructure.DataAccess.EntityConfigurations.Channels;

public class ChannelConfigurations: 
    IEntityTypeConfiguration<CategoryChannel>,
    IEntityTypeConfiguration<ForkChannel>,
    IEntityTypeConfiguration<TextChannel>,
    IEntityTypeConfiguration<VoiceChannel>,
    IEntityTypeConfiguration<CommunityChannel>,
    IEntityTypeConfiguration<OverwritesPermissionChannel>
{
    public void Configure(EntityTypeBuilder<CategoryChannel> builder)
        => builder.HasBaseType<OverwritesPermissionChannel>();

    public void Configure(EntityTypeBuilder<TextChannel> builder)
        => builder.HasBaseType<OverwritesPermissionChannel>();
    
    public void Configure(EntityTypeBuilder<VoiceChannel> builder)
        => builder.HasBaseType<OverwritesPermissionChannel>();
    
    public void Configure(EntityTypeBuilder<ForkChannel> builder)
        => builder.HasBaseType<CommunityChannel>();

    public void Configure(EntityTypeBuilder<CommunityChannel> builder)
        => CommunityChannelConfiguration(builder);    
    
    public void Configure(EntityTypeBuilder<OverwritesPermissionChannel> builder)
        => OverwritesChannelConfiguration(builder);
    
    static void OverwritesChannelConfiguration<TChannel>(EntityTypeBuilder<TChannel> builder) 
        where TChannel : OverwritesPermissionChannel
    {
        builder.HasBaseType<CommunityChannel>();
        builder.Ignore(nameof(OverwritesPermissionChannel.Overwrites));
        builder.OwnsMany<Overwrite>("_overwrites")
            .HasElementName(nameof(OverwritesPermissionChannel.Overwrites));
    }
    
    static void CommunityChannelConfiguration<TChannel>(EntityTypeBuilder<TChannel> builder) 
        where TChannel : CommunityChannel
    {
        builder.ToCollection(CollectionNames.Channels);

        builder.Ignore(x => x.DomainEvents);
        builder.Ignore(x => x.IsTransient);
        
        builder.HasDiscriminator(x=> x.Type)
            .HasValue<ForkChannel>(ChannelType.ForkChannel)
            .HasValue<TextChannel>(ChannelType.TextChannel)
            .HasValue<CategoryChannel>(ChannelType.CategoryChannel)
            .HasValue<VoiceChannel>(ChannelType.VoiceChannel)
            .IsComplete(false);

        builder.HasIndex(x => x.CommunityId);
        
        builder.Ignore(x => x.Overwrites);
        builder.Ignore(x => x.IsSyncing);
    }
    
}