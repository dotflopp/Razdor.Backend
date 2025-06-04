using Mediator;
using Razdor.Communities.Module.Contracts;

namespace Razdor.Communities.Infrastructure;

public class CommunitiesModule(IMediator mediator) : ICommunitiesModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommunitiesCommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(command);
    }

    public async Task ExecuteCommandAsync(ICommunitiesCommand command, CancellationToken cancellationToken = default)
    {
        await mediator.Send(command);
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(ICommunitiesQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(query);
    }
}