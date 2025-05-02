namespace Razdor.Shared.Domain.Exceptions;

public abstract class DomainException(
    ErrorCodes code,
    string? message = null,
    Exception? innerException = null
) : RazdorException(code, message, innerException);