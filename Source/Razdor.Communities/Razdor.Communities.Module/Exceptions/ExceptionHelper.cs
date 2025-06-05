using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public static class ExceptionHelper
{
    [DoesNotReturn]
    public static void ThrowInviteWithoutCommunity(Invite invite)
    {
        throw new DatabaseIntegrityBrokenException("Invite by id {invite.Id} without a Community {invite.CommunityId}\"");
    }
}