using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class AccessForbiddenException(
    string? message = null,
    Exception? innerException = null
) : ModuleException(ErrorCode.AccessForbidden, message, innerException);