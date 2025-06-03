using System.Text.Json.Serialization;
using Razdor.Messages.Domain;
using Razdor.Shared.Module;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MessageReferenceViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong MessageId
){
    public static MessageReferenceViewModel From(MessageReference messageReference)
    {
        return new MessageReferenceViewModel(
            messageReference.ChannelId,
            messageReference.MessageId
        );
    }
}