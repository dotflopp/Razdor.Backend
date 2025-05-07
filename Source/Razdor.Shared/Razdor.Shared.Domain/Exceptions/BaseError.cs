namespace Razdor.Shared.Domain.Exceptions;

public abstract class BaseError(ErrorCode code, string message)
{
    public ErrorCode Code { get; protected set; } = code;
    public string Message { get; protected set; } = message;
}