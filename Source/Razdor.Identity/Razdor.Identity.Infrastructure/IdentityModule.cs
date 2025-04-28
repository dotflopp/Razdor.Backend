
using Mediator;

using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Infrastructure;

public class IdentityModule(IMediator mediator) : IIdentityModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(IIdentityCommand<TResult> command)
        => await mediator.Send(command);

    public async Task ExecuteCommandAsync(IIdentityCommand command)
        => await mediator.Send(command);

    public async Task<TResult> ExecuteQueryAsync<TResult>(IIdentityQuery<TResult> query)
        => await mediator.Send(query);
}
