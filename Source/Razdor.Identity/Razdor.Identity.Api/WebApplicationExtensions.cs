using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Repositories;
using Razdor.Shared.Features;
using Razdro.Identity.DataAccess;

namespace Razdor.Identity.Api;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        AccessTokenFactoryOptions accessTokenOptions = new AccessTokenFactoryOptions(
            new DateTime(2025, 1, 1),
            Convert.FromBase64String(
                "K3UA5ta52VOeTguHAgYaw+5IV4KLUlflzx3sYjy8WpnLPsmR8oYsIHewP4U7cE/JBNRR9gNdGhaflBlJcGXA6lEu8ZdL1+x9muyI1nfuivA="
            )
        );
        
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, IdentityJsonSerializerContext.Default);
        });
        
        builder.Services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("LocalIdentity"));
        });
        builder.Services.AddSingleton<AccessTokenFactory>(
            provider => new AccessTokenFactory(accessTokenOptions)    
        );
        
        builder.Services.AddSingleton<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
        builder.Services.AddTransient<IUserRepository, UserEfRepository>();
        
        return builder;
    }
}