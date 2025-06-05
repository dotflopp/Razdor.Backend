using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class ResourceNotFoundException(
    Type resourceType, 
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.NotFound, message, innerException)
{
    public Type ResourceType { get; } = resourceType;

    [DoesNotReturn]
    public static void Throw<TResource>()
    {
        Type type = typeof(TResource);
        throw new ResourceNotFoundException(type, $"{type.Name} was not found.");
    }
    
    [DoesNotReturn]
    public static void Throw<TResource, TResourceId>(TResourceId resourceId)
    {
        Type type = typeof(TResource);
        throw new ResourceNotFoundException(type, $"{type.Name} by id {resourceId} was not found.");
    }

    
    public static void ThrowIfNull<TResource>([NotNull] TResource? resource)
    {
        if (resource is null)
            Throw<TResource>();
    }
    
    
    public static void ThrowIfNull<TResource, TId>([NotNull] TResource? resource, TId resourceId)
    {
        if (resource is null)
            Throw<TResource, TId>(resourceId);
    }
}