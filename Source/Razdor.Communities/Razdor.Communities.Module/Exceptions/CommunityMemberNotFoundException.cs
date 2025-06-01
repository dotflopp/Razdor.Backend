using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public sealed class CommunityMemberNotFoundException(
    string? message = null,
    Exception? innerException = null
) : ModuleException(ErrorCode.CommunityMemberNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong communityId, ulong userId)
    {
        throw new CommunityMemberNotFoundException($"The User({userId}) is not a member of the Community({communityId})");
    }
}