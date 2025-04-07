namespace Razdor.Shared.Domain;

public class RazdorException(
    string? message = null, 
    Exception? innerException = null
) : Exception(message, innerException);