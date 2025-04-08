using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Communities.DataAccess.EF;
using Razdor.Communities.DataAccess.EF.Repositories;
using Razdor.Communities.Domain.Repositories;

namespace Razdor.Communities.Services;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddKernelServices(
        this IServiceCollection services
    ) {
        services.AddDbContext<RazdorDataContext>(options =>
        {
            options.UseSqlite("Data Source=razdor.db");
        });

        services.AddTransient<IUserRepository, UsersRepository>();
        services.AddTransient<IChannelsRepository, ChannelsRepository>();
        services.AddTransient<IGuildsRepository, GuildsRepository>();

        return services;
    }
}