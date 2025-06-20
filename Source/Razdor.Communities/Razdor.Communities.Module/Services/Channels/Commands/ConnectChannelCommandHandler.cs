﻿using System.Text.Json.Serialization;
using Mediator;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public interface ISignalingService
{
    public Task<SessionViewModel> CreateUserSession(ulong channelId, ulong userId);
}

public record SessionViewModel(
    string Token
);

public class ConnectChannelCommandHandler(
    ISignalingService signalingService,
    IRequestSenderContext sender
): ICommandHandler<ConnectChannelCommand, SessionViewModel>
{
    public async ValueTask<SessionViewModel> Handle(ConnectChannelCommand command, CancellationToken cancellationToken)
    {
        return await signalingService.CreateUserSession(command.ChannelId, sender.User.Id);
    }
}