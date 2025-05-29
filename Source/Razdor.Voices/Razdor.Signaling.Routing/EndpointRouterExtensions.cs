using Razdor.Signaling.Routing.Signaling;

namespace Razdor.Signaling.Routing;

public static class EndpointRouterExtensions
{
    public static HubEndpointConventionBuilder MapSignalingHub(
        this IEndpointRouteBuilder routeBuilder,
        string pattern = "api/signaling"
    )
    {
        return routeBuilder.MapHub<SignalingHub>(pattern);
    }
}