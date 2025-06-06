using System.Text.Json.Serialization;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.PublicEvents.ViewModels.Invites;

public record InviteViewModel(
    string Id,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CreatorId,
    DateTimeOffset? ExpiresAt,
    DateTimeOffset CreatedAt,
    uint UsesCount,
    InviteCommunityViewModel Community
)
{
    public static InviteViewModel From(Invite invite, Community community)
    {
        return new InviteViewModel(
            invite.Id,
            invite.CreatorId,
            invite.ExpiresAt,
            invite.CreatedAt,
            invite.UsesCount,
            InviteCommunityViewModel.From(community)
        );
    }
}