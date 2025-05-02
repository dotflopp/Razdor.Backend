using Razdor.Signaling.Internal;

namespace Razdor.Signaling.Services.Signaling;

public class SignalingOneServiceProvider : ISignalingServiceProvider
{
    private readonly ISignalingInternalService _signaling;

    public SignalingOneServiceProvider(ISignalingInternalService service)
    {
        _signaling = service;
    }

    public Task<ISignalingInternalService?> FindSignalingServiceAsync(ulong serviceId)
    {
        if (serviceId == _signaling.Id)
            return Task.FromResult(_signaling)!;

        return Task.FromResult<ISignalingInternalService?>(null);
    }

    public Task<ISignalingInternalService> GetOptimalSignalingServiceAsync(ulong guildId, ulong channelId)
    {
        return Task.FromResult(_signaling);
    }
}