using Mediator;
using Microsoft.Extensions.Logging;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Authorization;

public class ChannelPermissionsHandler<TMessage, TResponse>(
    IRequestSenderContext sender,
    IChannelPermissionsAccessor channelPermissions,
    ILogger<ChannelPermissionsHandler<TMessage, TResponse>> logger
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IRequiredChannelPermissions
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    ){
        UserPermissions permissions;
        try
        {
            permissions = await channelPermissions.GetMemberPermissionsAsync(
                sender.User.Id,
                message.ChannelId,
                cancellationToken
            );
        }
        catch (ResourceNotFoundException ex) 
        {
            throw new AccessForbiddenException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unknown access channel permissions error.");
            throw new AccessForbiddenException("Unknown exception", ex);
        }

        if (!permissions.HasFlag(UserPermissions.Administrator) && !permissions.HasFlag(message.RequiredPermissions))
            throw new AccessForbiddenException($"{message.RequiredPermissions} permissions are required to perform the operation.");

        return await next(message, cancellationToken);
    }
}