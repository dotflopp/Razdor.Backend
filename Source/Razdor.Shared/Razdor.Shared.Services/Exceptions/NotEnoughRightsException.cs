using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class NotEnoughRightsException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.NotEnoughRights, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(UserPermissions requiredPermissions)
    {
        throw new NotEnoughRightsException($"Rights are not sufficient, the permissions {requiredPermissions} are required");    
    }
    
    [DoesNotReturn]
    public static void ThrowResourceHasHigherPriority()
    {
        throw new NotEnoughRightsException("The resource has a higher priority");
    }
    
}