using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Messages.Domain;
using Razdor.Messages.Infrastructure.DataAccess;
using Razdor.Messages.Module;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.DataAccess;
using Razdor.Shared.Infrastructure;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Messages.Infrastructure;

public static class Index
{
    public static IHostApplicationBuilder AddMessages(this IHostApplicationBuilder builder)
    {
        string messagingDb = builder.Configuration.GetConnectionString(DbNames.MessagingDb)!;
        builder.Services.AddMessagesServices(
            new MessagesOptions(
                messagingDb,
                DbNames.MessagingDb
            )
        );

        return builder;    
    }
    
    public static IServiceCollection AddMessagesServices(
        this IServiceCollection services,
        MessagesOptions moduleOptions
    ){
        services.AddDbContext<MessagesDbContext, MessagesMongoDbContext>(options =>
        {
            options.UseMongoDB(moduleOptions.ConnectionString, moduleOptions.DataBaseName);
            options.UseModel(MessagesMongoDbContextModel.Instance);
        });
        
        services.AddScoped<IMessagesRepository, MessagesRepository>();
        services.AddScoped<UnitOfWork<MessagesDbContext>>();
        services.AddScoped<IMessagesModule, MessagesModule>();
        
        return services;
    }
}