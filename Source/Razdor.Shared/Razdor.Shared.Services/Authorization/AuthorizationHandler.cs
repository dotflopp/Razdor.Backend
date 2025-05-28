using Mediator;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Shared.Module.Authorization;

public sealed class AuthorizationHandler<TMessage, TResponse>(
    IRequestSenderContextAccessor sender
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IAuthorizationRequiredMessage
{
    public ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (!sender.IsAuthenticated)
            throw new UnauthenticatedException("User authorization is required");
        
        return next(message, cancellationToken);
    }
}