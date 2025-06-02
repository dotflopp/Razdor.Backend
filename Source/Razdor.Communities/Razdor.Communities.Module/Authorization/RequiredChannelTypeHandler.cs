using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;

public class RequiredChannelTypeHandler<TMessage, TResponse>(
    CommunitiesDbContext context  
): IPipelineBehavior<TMessage, TResponse>
    where TMessage : IRequiredChannelType
{
    public async ValueTask<TResponse> Handle(
        TMessage message, 
        MessageHandlerDelegate<TMessage, TResponse> next, 
        CancellationToken cancellationToken
    )
    {
        ChannelType? channelType = context.Channels
            .Where(x => x.Id == message.ChannelId)
            .Select(x => x.Type)
            .FirstOrDefault();
        
        if (channelType == null)
            ChannelNotFoundException.Throw(message.ChannelId);

        if (!message.AllowedTypes.HasFlag(channelType))
            InvalidChannelOperationException.ThrowInvalidType();
            
        return await next(message, cancellationToken);
    }
}