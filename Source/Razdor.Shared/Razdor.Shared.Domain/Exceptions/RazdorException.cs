namespace Razdor.Shared.Domain.Exceptions;

public abstract class RazdorException(
    ErrorCodes code,
    string? message = null,
    Exception? innerException = null
) : Exception(message, innerException)
{
    public ErrorCodes Code => code;
}