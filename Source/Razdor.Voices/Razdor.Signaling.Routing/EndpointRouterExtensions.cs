using Razdor.Signaling.Routing.Signaling;

namespace Razdor.Signaling.Routing;

public static class EndpointRouterExtensions
{
    public static HubEndpointConventionBuilder MapSignalingHub(
        this IEndpointRouteBuilder routeBuilder,
        string pattern = "/signaling"
    )
    {
        return routeBuilder.MapHub<SignalingHub>(pattern);
    }
}