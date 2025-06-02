using Mediator;
using Microsoft.Extensions.Logging;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Authorization;

public class ChannelPermissionsHandler<TMessage, TResponse>(
    IRequestSenderContextAccessor sender,
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
                message.CommunityId,
                sender.User.Id,
                message.ChannelId,
                cancellationToken
            );
        }
        catch (CommunityMemberNotFoundException exception)
        {
            throw new AccessForbiddenException(exception.Message, exception);
        }
        catch (CommunityChannelNotFoundException exception)
        {
            throw new AccessForbiddenException(exception.Message, exception);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unknown access channel permissions error.");
            throw new AccessForbiddenException("Unknown exception", exception);
        }

        if (!permissions.HasFlag(UserPermissions.Administrator) && !permissions.HasFlag(message.RequiredPermissions))
            throw new AccessForbiddenException($"{message.RequiredPermissions} permissions are required to perform the operation.");

        return await next(message, cancellationToken);
    }
}