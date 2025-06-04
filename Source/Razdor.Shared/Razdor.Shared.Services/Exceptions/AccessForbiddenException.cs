using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public sealed class AccessForbiddenException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.AccessForbidden, message, innerException);