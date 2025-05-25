using Razdor.Communities.Domain.Invites;

namespace Razdor.Communities.Services.Communities.Commands.ViewModels;

public record InviteViewModel(
    string Id,
    ulong CreatorId,
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