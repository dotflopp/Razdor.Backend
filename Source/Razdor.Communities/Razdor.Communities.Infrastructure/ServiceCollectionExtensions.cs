using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Infrastructure.DataAccess;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.DataAccess;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommunityServices(this IServiceCollection services, CommunitiesOptions options)
    {
        services.AddScoped<ICommunityModule, CommunityModule>();
        
        services.AddDbContext<CommunityDataContext, CommunityMongoDataContext>(builder =>
            builder.UseMongoDB(options.ConnectionString, options.DataBaseName)
        );
        
        services.AddScoped<UnitOfWork<CommunityDataContext>>();
        services.AddScoped<ICommunitiesRepository, CommunitiesRepository>();
        services.AddScoped<ICommunityMembersRepository, CommunityMembersRepository>();
        
        return services;
    }
}