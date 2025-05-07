namespace Razdor.Shared.Domain.Exceptions;

public abstract class RazdorException(
    ErrorCode errorCode,
    string? message = null,
    Exception? innerException = null
) : Exception(message, innerException)
{
    public ErrorCode ErrorCode => errorCode;
}