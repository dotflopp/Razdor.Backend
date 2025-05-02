using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Identity.Module.Users.Queries.Exceptions;

public class UserNotFoundException(string? message = null, Exception? innerException = null) : ModuleException(ErrorCodes.UserNotFound, message, innerException)
{
    
}