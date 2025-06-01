using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Signaling.Internal;

namespace Razdor.Communities.Infrastructure.Signaling;

public class SignalingService(ISignalingServiceProvider signalingProvider): ISignalingService
{
    public async Task<SessionViewModel> CreateUserSession(ulong communityId, ulong channelId, ulong userId)
    {
        ISignalingInternalService signaling = await signalingProvider.GetOptimalSignalingServiceAsync(communityId, channelId);
        
        IRoom room = await signaling.CreateIfNotExistRoomAsync(channelId);
        IRoomSession session = await room.CreateUserSessionIfNotExistsAsync();
        
        return new SessionViewModel(session.SessionId);
    }
}