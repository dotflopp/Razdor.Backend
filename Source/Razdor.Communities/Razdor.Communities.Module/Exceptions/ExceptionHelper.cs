using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Invites;

namespace Razdor.Shared.Module.Exceptions;

public static class ExceptionHelper
{
    [DoesNotReturn]
    public static void ThrowInviteWithoutCommunity(Invite invite)
    {
        throw new DatabaseIntegrityBrokenException("Invite by id {invite.Id} without a Community {invite.CommunityId}\"");
    }
}