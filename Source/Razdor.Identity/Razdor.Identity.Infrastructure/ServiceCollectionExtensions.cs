using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure.DataAccess;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Services.Auth.AccessTokens;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Identity.Infrastructure;

public static class ServiceCollectionExtensions
{
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