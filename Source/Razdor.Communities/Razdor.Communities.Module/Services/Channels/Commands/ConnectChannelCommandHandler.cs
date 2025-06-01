using System.Text.Json.Serialization;
using Mediator;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public interface ISignalingService
{
    public Task<SessionViewModel> CreateUserSession(ulong communityId, ulong channelId, ulong userId);
}

public record SessionViewModel(
    [property:JsonPropertyName("SessionId")]
    string AccessToken
);

public class ConnectChannelCommandHandler(
    ISignalingService signalingService
): ICommandHandler<ConnectChannelCommand, SessionViewModel>
{
    public async ValueTask<SessionViewModel> Handle(ConnectChannelCommand command, CancellationToken cancellationToken)
    {
        return await signalingService.CreateUserSession(command.CommunityId, command.CommunityId, command.ChannelId);
    }
}