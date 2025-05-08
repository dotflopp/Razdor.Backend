using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class UnauthenticatedException(
    string? message = null,
    Exception? innerException = null
) : ModuleException(ErrorCode.Unauthorized, message, innerException);