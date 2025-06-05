using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Services.Users.Queries.Exceptions;

public class UserNotFoundException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.UserNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong userId)
    {
        throw new UserNotFoundException("The user with id 1 was not found.");
    }
}