namespace Razdor.Shared.Module.Exceptions;

public class DatabaseIntegrityBrokenException(
    string? message = null,
    Exception? innerException = null
): Exception(message, innerException);