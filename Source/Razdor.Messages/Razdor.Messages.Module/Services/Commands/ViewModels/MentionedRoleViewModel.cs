using System.Text.Json.Serialization;
using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Module;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MentionedRoleViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong RoleId)
{
    public static MentionedRoleViewModel From(MentionedRole role)
    {
        return new MentionedRoleViewModel(
            role.CommunityId,
            role.RoleId
        );
    }
}