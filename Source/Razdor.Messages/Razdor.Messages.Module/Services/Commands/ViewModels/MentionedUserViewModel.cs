using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

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