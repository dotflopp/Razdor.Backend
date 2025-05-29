using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Razdor.Communities.Api.Communities.Channels.ViewModels;
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
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId,
    string Name,
    bool IsSyncing,
    IEnumerable<OverwriteViewModel> Overwrites
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
        => new(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    public static VoiceChannelViewModel From(VoiceChannel channel)
        => new(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    public static ForkChannelViewModel From(ForkChannel channel)
        => new(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    public static CategoryChannelViewModel From(CategoryChannel channel)
        => new(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
}