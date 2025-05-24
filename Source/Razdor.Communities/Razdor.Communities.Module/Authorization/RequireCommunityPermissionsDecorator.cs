using Mediator;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Authorization;

public class RequireCommunityPermissionsDecorator<TMessage, TResponse>(
    IRequestSenderContext sender,
    IUserPermissionsAccessor permissionsAccessor    
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IPermissionsRequiredMessage
{

    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        UserPermissions permissions = await permissionsAccessor.GetUserCommunityPermissionsAsync(
            message.CommunityId, 
            sender.User.Id
        );

        if (permissions.HasFlag(message.RequiredPermissions))
            throw new AccessForbiddenException($"{message.RequiredPermissions} permissions are required to perform the operation.");
            
        return await next(message, cancellationToken);
    }
}