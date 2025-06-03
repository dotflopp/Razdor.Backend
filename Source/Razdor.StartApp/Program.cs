using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Models;
using Razdor.Api;
using Razdor.Api.AuthenticationScheme;
using Razdor.Api.Constraints;
using Razdor.Api.Middlewares;
using Razdor.Api.OpenAPI;
using Razdor.Api.Routes;
using Razdor.Api.Routes.Communities;
using Razdor.Api.Routes.Messaging;
using Razdor.Communities.Infrastructure;
using Razdor.Communities.Module.Authorization;
using Razdor.Identity.Infrastructure;
using Razdor.ServiceDefaults;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.RequestSenderContext;
using Scalar.AspNetCore;
using Razdor.Api.Serialization;
using Razdor.Messages.Infrastructure;
using Razdor.StartApp;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

// Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(0, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddScheme<AccessTokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
        "AccessTokenAuthentication",
        options => { }
    );

builder.Services.Configure<RouteOptions>(
    options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex")
);

// OpenApi configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddSchemaTransformer<StringTypesSchemaFilter>();
    options.AddDocumentTransformer((document, context, token) =>
    {
        foreach (var server in document.Servers.Where(x => x.Url.StartsWith("http://dotflopp.ru/")))
            server.Url = "https://dotflopp.ru/";
        
        return Task.CompletedTask;
    });
});

// Mediator
builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
    options.PipelineBehaviors =
    [
        typeof(AuthorizationHandler<,>),
        typeof(CommunityPermissionsHandler<,>),
        typeof(ChannelPermissionsHandler<,>),
        typeof(RequiredChannelTypeHandler<,>)
    ];
});

//Cache
builder.Services.AddHybridCache();

// Ulong Constraint for api routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.ConstraintMap.Add(
        ULongRouteConstraint.Name,
        typeof(ULongRouteConstraint)
    );
});


builder.Services.ConfigureHttpJsonOptions(options =>
{
    IList<IJsonTypeInfoResolver> typeResolver = options.SerializerOptions.TypeInfoResolverChain;
    typeResolver.Add(SharedJsonSerializerContext.Default);
    typeResolver.Add(CommunitiesJsonSerializerContext.Default);
    typeResolver.Add(IdentityJsonSerializerContext.Default);
    typeResolver.Add(MessagesJsonSerializationContext.Default);
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(10)));
});

// Snowflake Generator
builder.Services.AddSingleton(
    new SnowflakeGenerator(0, new DateTime(2025, 1, 1))
);

//UserContext Accessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRequestSenderContextAccessor, RequestSenderContextAccessor>();

// Identity services
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

// Community Services
string communityDb = builder.Configuration.GetConnectionString(DbNames.CommunityDb)!;
builder.Services.AddCommunityServices(
    new CommunitiesOptions(
        communityDb,
        DbNames.CommunityDb
    )
);


string messagingDb = builder.Configuration.GetConnectionString(DbNames.MessagingDb)!;
builder.Services.AddMessagesServices(
    new MessagesOptions(
        messagingDb,
        DbNames.MessagingDb
    )
);

WebApplication app = builder.Build();

app.UseCors();
// Map OpenApi and Swagger UI
app.MapOpenApi("/api/swagger/{documentName}/swagger.json");

app.MapScalarApiReference("/api/swagger", options =>
{
    options.WithOpenApiRoutePattern("/api/swagger/{documentName}/swagger.json");
    options.AddDocument("v1", "Main API");
});

app.UseCustomNotAuthorizedResponse();
app.UseAuthentication();
app.UseAuthorization();

app.UseRazdorExceptionHandlerMiddleware();

app.MapIdentityApi();
app.MapCommunitiesApi();
app.MapMessagingApi();

app.UseNonExistentRouteResponse();

app.Run();