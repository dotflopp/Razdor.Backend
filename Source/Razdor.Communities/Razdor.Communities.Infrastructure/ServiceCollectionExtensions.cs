using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Communities.Infrastructure.DataAccess;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.DataAccess;

namespace Razdor.Communities.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommunityServices(this IServiceCollection services, CommunitiesOptions options)
    {
        services.AddScoped<ICommunityModule, CommunityModule>();
        
        services.AddDbContext<CommunityDataContext, CommunityMongoDataContext>(builder =>
            builder.UseMongoDB(options.ConnectionString, options.DataBaseName)
        );
        
        return services;
    }
}