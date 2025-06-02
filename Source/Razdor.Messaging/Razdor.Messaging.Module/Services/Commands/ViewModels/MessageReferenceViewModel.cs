using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Razdor.Messaging.Domain;
using Razdor.Shared.Module;

namespace Razdor.Messaging.Module.Services.Commands.ViewModels;

public record MessageReferenceViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId,
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