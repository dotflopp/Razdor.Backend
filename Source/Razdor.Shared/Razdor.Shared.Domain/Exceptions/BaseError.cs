namespace Razdor.Shared.Domain.Exceptions;

public abstract class BaseError(ErrorCodes code, string message)
{
    public ErrorCodes Code { get; protected set; } = code;
    public string Message { get; protected set; } = message;
}