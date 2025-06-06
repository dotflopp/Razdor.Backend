using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Razdor.Communities.Infrastructure;
using Razdor.Communities.Module.Authorization;
using Razdor.Identity.Infrastructure;
using Razdor.ServiceDefaults;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.RequestSenderContext;
using Scalar.AspNetCore;
using Razdor.Messages.Infrastructure;
using Razdor.RestApi;
using Razdor.RestApi.AuthenticationScheme;
using Razdor.RestApi.Constraints;
using Razdor.RestApi.ExceptionHandleMiddlewares;
using Razdor.RestApi.Multipart;
using Razdor.RestApi.OpenAPI;
using Razdor.RestApi.Routes;
using Razdor.Shared.Infrastructure;
using Razdor.Shared.Infrastructure.Events;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module.Media;
using Razdor.SignalR;
using Razdor.StartApp;
using CommunitiesJsonSerializerContext = Razdor.RestApi.Serialization.CommunitiesJsonSerializerContext;
using IdentityJsonSerializerContext = Razdor.RestApi.Serialization.IdentityJsonSerializerContext;
using MessagesJsonSerializationContext = Razdor.RestApi.Serialization.MessagesJsonSerializationContext;
using SharedJsonSerializerContext = Razdor.RestApi.Serialization.SharedJsonSerializerContext;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

// Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(0, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("dotflopp.v");
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


builder.Services.AddSignalR();

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
        typeof(RequiredChannelTypeHandler<,>),
        typeof(PerfomanceLoggerBhavior<,>)
    ];
});

builder.Services.AddScoped<InMemoryEventBus>();
builder.Services.AddScoped<IEventBus, InMemoryEventBusClient>();

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
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    
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

builder.Services.AddScoped<ContentWithFilesAccessor>();

builder.Services.AddScoped<IFileStore, LocalFileStore>();
// Snowflake Generator
builder.Services.AddSingleton(
    new SnowflakeGenerator(0, new DateTime(2025, 1, 1))
);

//UserContext Accessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRequestSenderContext, RequestSenderContext>();

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

app.MapSignalRGateway();
app.MapIdentityApi();
app.MapCommunitiesApi();
app.MapMessagingApi();

app.UseNonExistentRouteResponse();

app.Run();