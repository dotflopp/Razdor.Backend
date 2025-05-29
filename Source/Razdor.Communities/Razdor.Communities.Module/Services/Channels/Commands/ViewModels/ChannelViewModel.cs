using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Services.Channels.Commands.ViewModels;
using Razdor.Shared.Module;

namespace Razdor.Communities.Services.Services.Channels.Commands;

[JsonDerivedType(typeof(ForkChannelViewModel))]
[JsonDerivedType(typeof(TextChannelViewModel))]
[JsonDerivedType(typeof(VoiceChannelViewModel))]
[JsonDerivedType(typeof(CategoryChannelViewModel))]
public abstract record ChannelViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,   
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    [property:JsonConverter(typeof(JsonStringEnumConverter))]
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId,
    bool IsSyncing,
    IEnumerable<Overwrite> Overwrites
){
    public static ChannelViewModel From(CommunityChannel channel)
        => channel switch
        {
            TextChannel textChannel => From(textChannel),
            CategoryChannel categoryChannel => From(categoryChannel),
            ForkChannel forkChannel => From(forkChannel),
            VoiceChannel voiceChannel => From(voiceChannel),
            _ => throw new ArgumentException(nameof(channel)),
        };
    
    public static TextChannelViewModel From(TextChannel channel)
        => new TextChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.IsSyncing, channel.Overwrites);
    public static VoiceChannelViewModel From(VoiceChannel channel)
        => new VoiceChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.IsSyncing, channel.Overwrites);
    public static ForkChannelViewModel From(ForkChannel channel)
        => new ForkChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.IsSyncing, channel.Overwrites);
    public static CategoryChannelViewModel From(CategoryChannel channel)
        => new CategoryChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.IsSyncing, channel.Overwrites);
}