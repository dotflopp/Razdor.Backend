using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Identity.Module.Users.Queries.Exceptions;

public class UserNotFoundException(
    string? message = null,
    Exception? innerException = null
) : ModuleException(ErrorCode.UserNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong userId)
    {
        throw new UserNotFoundException("The user with id 1 was not found.");
    }
}