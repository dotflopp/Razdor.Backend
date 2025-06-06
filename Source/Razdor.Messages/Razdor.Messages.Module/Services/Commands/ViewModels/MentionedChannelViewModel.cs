using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MentionedChannelViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId
){

    public static MentionedChannelViewModel From(MentionedChannel channel)
    {
        return new MentionedChannelViewModel(
            channel.ChannelId
        );
    }
}