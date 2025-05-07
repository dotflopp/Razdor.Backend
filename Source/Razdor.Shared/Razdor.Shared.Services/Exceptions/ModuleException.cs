using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public abstract class ModuleException(
    ErrorCode errorCode,
    string? message = null,
    Exception? innerException = null
) : RazdorException(errorCode, message, innerException);