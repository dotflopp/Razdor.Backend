using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public class MentionedChannelViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId
){

    public static MentionedChannelViewModel From(MentionedChannel channel)
    {
        return new MentionedChannelViewModel(
            channel.ChannelId,
            channel.CommunityId
        );
    }
}