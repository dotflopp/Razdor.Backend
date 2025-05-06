namespace Razdor.Shared.Module.DbExceptions;

public class UniqueConstraintException(
    string? message = null,
    Exception? innerException = null
) : Exception(message, innerException);