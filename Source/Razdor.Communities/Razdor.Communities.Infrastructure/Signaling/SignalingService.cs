using Razdor.Communities.Module.Services.Channels.Commands;

namespace Razdor.Communities.Infrastructure.Signaling;

public class SignalingService(): ISignalingService
{
    public async Task<SessionViewModel> CreateUserSession(ulong channelId, ulong userId)
    {
        throw new NotImplementedException();
    }
}