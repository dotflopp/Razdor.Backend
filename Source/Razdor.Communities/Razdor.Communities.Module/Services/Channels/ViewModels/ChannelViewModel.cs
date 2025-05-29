using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Module;

namespace Razdor.Communities.Services.Services.Channels.ViewModels;

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
)
{
    public static ChannelViewModel From(CommunityChannel channel)
    {
        return channel switch
        {
            TextChannel textChannel => From(textChannel),
            CategoryChannel categoryChannel => From(categoryChannel),
            ForkChannel forkChannel => From(forkChannel),
            VoiceChannel voiceChannel => From(voiceChannel),
            _ => throw new ArgumentException(nameof(channel))
        };
    }

    public static TextChannelViewModel From(TextChannel channel)
    {
        return new TextChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    }
    public static VoiceChannelViewModel From(VoiceChannel channel)
    {
        return new VoiceChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    }
    public static ForkChannelViewModel From(ForkChannel channel)
    {
        return new ForkChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    }
    public static CategoryChannelViewModel From(CategoryChannel channel)
    {
        return new CategoryChannelViewModel(channel.Id, channel.CommunityId, channel.Type, channel.ParentId, channel.Name, channel.IsSyncing, channel.Overwrites.Select(OverwriteViewModel.From));
    }
}