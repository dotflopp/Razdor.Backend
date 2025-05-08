using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Identity.Api;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.Sql;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.DataAccess;

namespace Razdor.Identity.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection collection,
        IdentityModuleOptions moduleOptions)
    {
        var accessTokenOptions = new AccessTokenSourceOptions(
            moduleOptions.AccessTokenStartDate, //new DateTime(2025, 1, 1),
            moduleOptions.AccessTokenSecurityKey
        );

        collection.AddTransient<IIdentityModule, IdentityModule>();
        
        collection.AddSingleton(_ => new AccessTokenSource(accessTokenOptions));

        collection.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, IdentityJsonSerializerContext.Default);
        });

        collection.AddDbContext<IIdentityDbContext, IdentitySqlliteDbContext>(options =>
        {
            options.UseSqlite(moduleOptions.SqlConnectionString);
        });
        collection.AddTransient<IUsersCounter, UsersEfCounter>();
        collection.AddTransient<IUserRepository, UserEfRepository>();

        collection.AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
        return collection;
    }
}