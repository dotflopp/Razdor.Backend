using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Infrastructure.Authorization;
using Razdor.Communities.Infrastructure.DataAccess;
using Razdor.Communities.Infrastructure.Signaling;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Members;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommunityServices(this IServiceCollection services, CommunitiesOptions options)
    {
        services.AddScoped<ICommunityModule, CommunityModule>();

        services.AddDbContext<CommunitiesDbContext, CommunitiesMongoDbContext>(builder =>
        {
            builder.UseMongoDB(options.ConnectionString, options.DataBaseName);
            builder.UseModel(CommunitiesMongoDbContextModel.Instance);
        });

        services.AddScoped<UnitOfWork<CommunitiesDbContext>>();
        services.AddScoped<ICommunitiesRepository, CommunitiesRepository>();
        services.AddScoped<ICommunityMembersRepository, CommunityMembersRepository>();
        services.AddScoped<ICommunityPermissionsAccessor, CachedCommunityPermissionsAccessor>();
        services.AddScoped<IChannelPermissionsAccessor, CachedChannelPermissionsAccessor>();
        services.AddScoped<IInvitesRepository, InvitesRepository>();
        services.AddScoped<ICommunityChannelsRepository, CommunityChannelsRepository>();
        services.AddScoped<ISignalingService, SignalingService>();
        services.AddScoped<IUserProfileFiller, CachedUserProfileFiler>();

        return services;
    }
}