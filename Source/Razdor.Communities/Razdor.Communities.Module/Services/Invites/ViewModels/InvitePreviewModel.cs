using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Communities.ViewModels;

public sealed record InvitePreviewModel(
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
    public static InvitePreviewModel From(Invite invite)
    {
        return new InvitePreviewModel(
            invite.Id,
            invite.CreatorId,
            invite.CommunityId,
            invite.ExpiresAt,
            invite.CreatedAt,
            invite.UsesCount
        );
    }
}