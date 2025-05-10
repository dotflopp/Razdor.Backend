namespace Razdor.Communities.Api.Communities.Channels;

public static class ChannelsRouter
{
    public static IEndpointRouteBuilder MapChannels(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("{communityId}/channels");

        api.MapGet("/", GetCommunityChannels);
        api.MapPost("/", CreateCommunityChannel);
        
        return builder;
    }

    private static Task CreateCommunityChannel(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetCommunityChannels(HttpContext context)
    {
        throw new NotImplementedException();
    }
}