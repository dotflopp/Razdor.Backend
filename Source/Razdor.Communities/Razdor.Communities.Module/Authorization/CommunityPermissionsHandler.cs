using System.Reflection.Metadata.Ecma335;
using Mediator;
using Microsoft.Extensions.Logging;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Exceptions;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Authorization;

public sealed class CommunityPermissionsHandler<TMessage, TResponse>(
    IRequestSenderContextAccessor senderContext,
    ICommunityPermissionsAccessor communityPermissions,
    ILogger<CommunityPermissionsHandler<TMessage, TResponse>> logger
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IRequiredCommunityPermissionsMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message, 
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    ){
        UserPermissions permissions;
        try
        {
            permissions = await communityPermissions.GetMemberPermissionsAsync(
                senderContext.User.Id,
                message.CommunityId, cancellationToken
            );
        }
        catch (CommunityMemberNotFoundException exception)
        {
            throw new AccessForbiddenException(exception.Message, exception);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Access community permissions error.");
            throw new AccessForbiddenException("Unknown exception", exception);
        }
        
        if (!permissions.HasFlag(UserPermissions.Administrator) && !permissions.HasFlag(message.RequiredPermissions))
            throw new AccessForbiddenException($"{message.RequiredPermissions} permissions are required to perform the operation.");
            
        return await next(message, cancellationToken);
    }
}