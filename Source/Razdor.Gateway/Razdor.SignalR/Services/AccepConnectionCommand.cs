using Mediator;

namespace Razdor.SignalR.Services;

public record AccepConnectionCommand(
    string ConnectionId    
): ICommand<Unit>;