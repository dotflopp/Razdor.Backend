﻿using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Messages.PublicEvents.ViewModels;

public record MentionedUserViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId
){
    public static MentionedUserViewModel From(MentionedUser user)
    {
        return new MentionedUserViewModel(
            user.UserId    
        );
    }
}