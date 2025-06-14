using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Services.Auth.AccessTokens;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Identity.Infrastructure;

public static class Index
{
    public static IHostApplicationBuilder AddIdentity(this IHostApplicationBuilder builder)
    {
        string identityDb = builder.Configuration.GetConnectionString("identitydb")!;
        builder.Services.AddIdentityServices(
            new IdentityModuleOptions(
                new DateTime(2025, 1, 1),
                Convert.FromBase64String(
                    "K3UA5ta52VOeTguHAgYaw+5IV4KLUlflzx3sYjy8WpnLPsmR8oYsIHewP4U7cE/JBNRR9gNdGhaflBlJcGXA6lEu8ZdL1+x9muyI1nfuivA="
                ),
                identityDb
            )
        );
        
        return builder;
    }

    public static IServiceCollection AddIdentityServices(
        this IServiceCollection collection,
        IdentityModuleOptions moduleOptions
    )
    {
        var accessTokenOptions = new AccessTokenSourceOptions(
            moduleOptions.AccessTokenStartDate, //new DateTime(2025, 1, 1),
            moduleOptions.AccessTokenSecurityKey
        );

        collection.AddTransient<IIdentityModule, IdentityModule>();

        collection.AddSingleton(_ => new AccessTokenSource(accessTokenOptions));

        collection.AddTransient<UnitOfWork<IdentityDbContext>>();
        collection.AddDbContext<IdentityDbContext, IdentityPostgresDbContext>(options =>
        {
            options.UseNpgsql(moduleOptions.ConnectionString);
            options.UseModel(IdentityPostgresDbContextModel.Instance);
        });

        collection.AddTransient<IUsersCounter, UsersEfCounter>();
        collection.AddTransient<IUserRepository, UserEfRepository>();

        collection.AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
        return collection;
    }
}