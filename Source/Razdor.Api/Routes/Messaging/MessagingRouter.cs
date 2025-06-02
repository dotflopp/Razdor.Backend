namespace Razdor.Api.Routes.Messaging;

public static class MessagingRouter
{
    public static IEndpointRouteBuilder MapMessagingApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    )
    {
        IEndpointRouteBuilder api = builder
            .NewVersionedApi("messages")
            .HasApiVersion(0.1)
            .MapGroup("/api/")
            .WithTags("Messaging");

        api.MapMessages();
    
        return builder;
    }

    private static IEndpointRouteBuilder MapMessages(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup("channel/{channelId:ulong}/messages");
        api.MapPost("/", SendMessageAsync);   
        
        return builder;
    }
    
    private static Task SendMessageAsync()
    {
        throw new NotImplementedException();
    }
}