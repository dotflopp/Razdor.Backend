using Razdor.RestApi.Routes.Auth;
using Razdor.RestApi.Routes.Channels;
using Razdor.RestApi.Routes.Channels.Overwrites;
using Razdor.RestApi.Routes.Communities;
using Razdor.RestApi.Routes.Communities.Members;
using Razdor.RestApi.Routes.Communities.Members.Roles;
using Razdor.RestApi.Routes.Communities.Roles;
using Razdor.RestApi.Routes.Invites;
using Razdor.RestApi.Routes.Messages;
using Razdor.RestApi.Routes.Users;

namespace Razdor.RestApi.Routes;

public static class ApiRouter
{
    public static IEndpointRouteBuilder MapIdentityApi(this IEndpointRouteBuilder router)
    {
        RouteGroupBuilder api = router
            .NewVersionedApi("identity")
            .HasApiVersion(0.1)
            .MapGroup("/api/")
            .WithTags("Identity");
        
        api.MapAuth();
        api.MapUsers();
        return api;
    }
    
    public static IEndpointRouteBuilder MapCommunitiesApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    )
    {
        IEndpointRouteBuilder api = builder
            .NewVersionedApi("communities")
            .HasApiVersion(0.1)
            .MapGroup("/api/")
            .WithTags("Communities");

        api.MapInvites();
        api.MapCommunities();
        api.MapCommunityInvites();
        api.MapCommunityChannels();
        api.MapChannelOverwrites();
        api.MapCommunityMembers();
        api.MapCommunityRoles();
        api.MepCommunityMemberRoles();

        return builder;
    }

    
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
}