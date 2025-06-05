using Razdor.Api.Routes.Auth;
using Razdor.Api.Routes.Channels;
using Razdor.Api.Routes.Channels.Overwrites;
using Razdor.Api.Routes.Communities;
using Razdor.Api.Routes.Communities.Members;
using Razdor.Api.Routes.Communities.Members.Roles;
using Razdor.Api.Routes.Communities.Roles;
using Razdor.Api.Routes.Invites;
using Razdor.Api.Routes.Messages;
using Razdor.Api.Routes.Users;

namespace Razdor.Api.Routes;

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