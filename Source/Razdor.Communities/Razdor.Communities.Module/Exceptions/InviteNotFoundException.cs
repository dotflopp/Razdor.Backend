using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Services.Exceptions;

public sealed class InviteNotFoundException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.InviteNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(string inviteId)
    {
        throw new InviteNotFoundException($"Invite with {inviteId} ID does not exist");
    }
}