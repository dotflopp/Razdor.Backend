using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Razdor.Communities.Infrastructure;
using Razdor.Communities.Module.Authorization;
using Razdor.Identity.Infrastructure;
using Razdor.Messages.Infrastructure;
using Razdor.RestApi;
using Razdor.RestApi.AuthenticationScheme;
using Razdor.RestApi.Constraints;
using Razdor.RestApi.Multipart;
using Razdor.RestApi.OpenAPI;
using Razdor.RestApi.Serialization;
using Razdor.Shared.Infrastructure;
using Razdor.Shared.Infrastructure.Events;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.StartApp;

public static class AppBuilderExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        
        builder.Services.AddScoped<IRequestSenderContext, RequestSenderContext>();

        builder.Services.AddHybridCache();

        builder.Services.AddScoped<EventBusSubscriber>();
        builder.Services.AddScoped<IEventBus, InMemoryEventBus>();
        
        builder.Services.AddScoped<IFileStore, LocalFileStore>();

        builder.Services.AddSingleton(
            new SnowflakeGenerator(0, new DateTime(2025, 1, 1))
        );
        
        return builder
            .AddMediator()
            .AddIdentity()
            .AddCommunities()
            .AddMessages();
    }

    public static IHostApplicationBuilder AddAspServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ContentWithFilesParser>();
        
        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(10)));
        });

        
        builder.Services.ConfigureHttpJsonOptions(
            options => options.SerializerOptions.UseDefaults());
        
        return builder.AddCors()
            .AddAuth()
            .AddApiVersioning()
            .AddSignalR()
            .ConfigureRouting()
            .AddOpenApi();
    }

    public static IHostApplicationBuilder AddCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition");
            });
        });
        return builder;
    }

    public static IHostApplicationBuilder AddApiVersioning(this IHostApplicationBuilder builder)
    {
        // Api Versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(0, 1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new MediaTypeApiVersionReader("dotflopp.v");
        });
        return builder;
    }

    public static IHostApplicationBuilder AddSignalR(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSignalR()
            .AddJsonProtocol(options =>
                options.PayloadSerializerOptions.UseDefaults());
        
        return builder;
    }

    public static void UseDefaults(this JsonSerializerOptions jsonOptions)
    {
        jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        
        IList<IJsonTypeInfoResolver> typeResolver = jsonOptions.TypeInfoResolverChain;
        typeResolver.Add(SharedJsonSerializerContext.Default);
        typeResolver.Add(CommunitiesJsonSerializerContext.Default);
        typeResolver.Add(IdentityJsonSerializerContext.Default);
        typeResolver.Add(MessagesJsonSerializationContext.Default);
    }

    public static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
    {
        
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication()
            .AddScheme<AccessTokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
                "AccessTokenAuthentication",
                options => { }
            );
        
        return builder;
    }

    public static IHostApplicationBuilder ConfigureRouting(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(
            options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex")
        );
        
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.ConstraintMap.Add(
                ULongRouteConstraint.Name,
                typeof(ULongRouteConstraint)
            );
        });
        return builder;
    }

    public static IHostApplicationBuilder AddOpenApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddSchemaTransformer<StringTypesSchemaFilter>();
            options.AddDocumentTransformer((document, context, token) =>
            {
                foreach (var server in document.Servers.Where(x => x.Url.StartsWith("http://dotflopp.ru/")))
                    server.Url = "https://dotflopp.ru/";

                string serverUrl = document.Servers.FirstOrDefault()?.Url ?? string.Empty;
                string signalRTestUrl = $"{serverUrl}api/signalr-connection-listener.html";
        
                document.Info ??= new();
                document.Info.Description = $"For signalR test {signalRTestUrl}";
       
                return Task.CompletedTask;
            });
        });
        
        return builder;
    }

    public static IHostApplicationBuilder AddMediator(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.PipelineBehaviors =
            [
                typeof(AuthorizationHandler<,>),
                typeof(CommunityPermissionsHandler<,>),
                typeof(ChannelPermissionsHandler<,>),
                typeof(RequiredChannelTypeHandler<,>),
                typeof(PerfomanceLoggerHandler<,>)
            ];
        });
        return builder;
    }
}