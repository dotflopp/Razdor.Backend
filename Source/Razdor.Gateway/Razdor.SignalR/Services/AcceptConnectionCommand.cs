using Mediator;

namespace Razdor.SignalR.Services;

public record AcceptConnectionCommand(
    string ConnectionId    
): ICommand<Unit>;