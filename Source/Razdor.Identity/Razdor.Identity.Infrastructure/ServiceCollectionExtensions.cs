using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Identity.Api;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess.Sql;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Contracts;

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

        collection.AddDbContext<IdentitySqlliteDbContext>(options =>
        {
            options.UseSqlite(moduleOptions.SqlConnectionString);
        });

        collection.AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
        collection.AddTransient<IUserRepository, UserEfRepository>();

        return collection;
    }
}