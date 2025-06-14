using Microsoft.AspNetCore.Http.Connections;

namespace Razdor.SignalR;

public static class Router
{
    public static IEndpointRouteBuilder MapSignalRGateway(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<ConnectionHub>("/api/signalr", options =>
        {
            options.AllowStatefulReconnects = true;
            options.CloseOnAuthenticationExpiration = false; 
            options.Transports = HttpTransportType.WebSockets;
        }).RequireAuthorization();
        return endpoints;
    }
}