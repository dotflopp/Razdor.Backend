using System.Diagnostics;
using Mediator;
using Microsoft.Extensions.Logging;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Authorization;

public sealed class CommunityPermissionsHandler<TMessage, TResponse>(
    IRequestSenderContext senderContext,
    ICommunityPermissionsAccessor communityPermissions,
    ILogger<CommunityPermissionsHandler<TMessage, TResponse>> logger
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IRequiredCommunityPermissions
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        UserPermissions permissions;
        try
        {
            permissions = await communityPermissions.GetMemberPermissionsAsync(
                message.CommunityId,
                senderContext.User.Id,
                cancellationToken
            );
        }
        catch (ResourceNotFoundException ex)
        {
            throw new AccessForbiddenException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unknown access community permissions error.");
            throw new AccessForbiddenException("Unknown exception", ex);
        }
        finally
        {
            stopwatch.Stop();
            logger.LogDebug($"Channel rights request passed {stopwatch.ElapsedMilliseconds}ms");
        }

        if (!permissions.HasFlag(UserPermissions.Administrator) && !permissions.HasFlag(message.RequiredPermissions))
            throw new AccessForbiddenException($"{message.RequiredPermissions} permissions are required to perform the operation.");

        return await next(message, cancellationToken);
    }
}