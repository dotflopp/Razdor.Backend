﻿using System.Text.Json.Serialization;
using Razdor.Messages.Domain;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Messages.PublicEvents.ViewModels;

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