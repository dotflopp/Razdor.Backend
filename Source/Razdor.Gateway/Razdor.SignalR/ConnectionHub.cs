using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.Contracts;
using Razdor.Gateways.PublicEvents;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module.RequestSenderContext;
using Razdor.SignalR.Services;

namespace Razdor.SignalR;

public sealed partial class ConnectionHub(
    IRequestSenderContext sender,
    IEventBus eventBus,
    IMediator mediator
): Hub<IRazdorClient>
{
    public override async Task OnConnectedAsync()
    {
        await eventBus.Publish(new UserConnectedPublicEvent(sender.User.Id));
        await mediator.Send(new AcceptConnectionCommand(Context.ConnectionId));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await eventBus.Publish(new UserDisconnectedPublicEvent(sender.User.Id));
    }
}