using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Module;

namespace Razdor.Communities.Services.Communities.Commands.ViewModels;

public sealed record InviteViewModel(
    string Id,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CreatorId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    DateTimeOffset? ExpiresAt,
    DateTimeOffset CreatedAt,
    uint UsesCount
)
{
    public static InviteViewModel From(Invite invite)
    {
        return new InviteViewModel(
            invite.Id,
            invite.CreatorId,
            invite.CommunityId,
            invite.ExpiresAt,
            invite.CreatedAt,
            invite.UsesCount
        );
    }
}