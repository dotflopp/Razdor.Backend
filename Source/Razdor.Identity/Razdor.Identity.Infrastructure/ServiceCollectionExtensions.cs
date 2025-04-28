using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Infrastructure;
using Razdor.Shared.Features;

namespace Razdor.Identity.Api;

public static class WebApplicationExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection collection, IdentityModuleOptions moduleOptions)
    {
        AccessTokenFactoryOptions accessTokenOptions = new AccessTokenFactoryOptions(
            moduleOptions.AccessTokenStartDate, //new DateTime(2025, 1, 1),
            moduleOptions.AccessTokenSecurityKey
        );
        
        collection.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, IdentityJsonSerializerContext.Default);
        });
        
        collection.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlite(moduleOptions.SqlConnectionString);
        });
        
        collection.AddSingleton<AccessTokenFactory>(
            provider => new AccessTokenFactory(accessTokenOptions)    
        );
        
        collection.AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
        collection.AddTransient<IUserRepository, UserEfRepository>();
        
        return collection;
    }
}