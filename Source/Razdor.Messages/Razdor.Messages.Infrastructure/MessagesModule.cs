using Mediator;
using Razdor.Messages.Module.Contracts;

namespace Razdor.Messages.Infrastructure;

public class MessagesModule(IMediator mediator): IMessagesModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(IMessagesCommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(command, cancellationToken);
    }
    public async Task ExecuteCommandAsync(IMessagesCommand command, CancellationToken cancellationToken = default)
    {
        await mediator.Send(command, cancellationToken);
    }
    public async Task<TResult> ExecuteQueryAsync<TResult>(IMessagesQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(query, cancellationToken);
    }
}