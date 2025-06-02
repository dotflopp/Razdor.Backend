using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Infrastructure.DataAccess;
using Razdor.Messaging.Module.Contracts;
using Razdor.Messaging.Module.DataAccess;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Messaging.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingServices(
        this IServiceCollection services,
        MessagingOptions moduleOptions
    ){
        services.AddDbContext<MessagingDataContext, MessagingMongoDBContext>(options =>
        {
            options.UseMongoDB(moduleOptions.ConnectionString, moduleOptions.DataBaseName);
        });
        
        services.AddScoped<IMessagesRepository, MessagesRepository>();
        services.AddScoped<UnitOfWork<MessagingDataContext>>();
        services.AddScoped<IMessagingModule, MessagingModule>();
        return services;
    }
}