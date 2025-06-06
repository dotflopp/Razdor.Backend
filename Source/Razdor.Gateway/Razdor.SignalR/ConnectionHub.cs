using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.RequestSenderContext;
using Razdor.SignalR.Services;

namespace Razdor.SignalR;

public sealed partial class ConnectionHub(
    IRequestSenderContext sender,
    IMediator mediator
): Hub<IRazdorClient>
{
    public override async Task OnConnectedAsync()
    {
        await mediator.Send(new AccepConnectionCommand(Context.ConnectionId));
    }
}