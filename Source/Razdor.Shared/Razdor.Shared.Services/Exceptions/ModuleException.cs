using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public abstract class ModuleException(
    ErrorCodes code,
    string? message = null,
    Exception? innerException = null
) : RazdorException(code, message, innerException);