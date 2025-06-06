using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Messages.PublicEvents.ViewModels;

public record MentionedRoleViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong RoleId)
{
    public static MentionedRoleViewModel From(MentionedRole role)
    {
        return new MentionedRoleViewModel(
            role.RoleId
        );
    }
}