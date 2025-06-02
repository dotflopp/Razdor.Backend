using Mediator;
using Razdor.Messaging.Module.Contracts;

namespace Razdor.Messaging.Infrastructure;

public class MessagingModule(IMediator mediator): IMessagingModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(IMessagingCommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(command, cancellationToken);
    }
    public async Task ExecuteCommandAsync(IMessagingCommand command, CancellationToken cancellationToken = default)
    {
        await mediator.Send(command, cancellationToken);
    }
    public async Task<TResult> ExecuteQueryAsync<TResult>(IMessagingQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(query, cancellationToken);
    }
}