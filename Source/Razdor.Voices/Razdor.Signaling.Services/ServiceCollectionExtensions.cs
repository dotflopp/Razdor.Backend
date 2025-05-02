using Microsoft.Extensions.DependencyInjection;
using Razdor.Signaling.Internal;
using Razdor.Signaling.Services.Signaling;

namespace Razdor.Signaling.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSignalingServices(
        this IServiceCollection services,
        string address
    )
    {
        services.AddSingleton<ISignalingServiceProvider, SignalingOneServiceProvider>();

        services.AddSingleton<ISignalingInternalService>(
            new SignalingInternalService(address)
        );

        return services;
    }
}