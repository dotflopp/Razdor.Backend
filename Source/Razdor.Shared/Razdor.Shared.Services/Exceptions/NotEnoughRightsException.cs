using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class NotEnoughRightsException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.NotEnoughRights, message, innerException)
{
    
}