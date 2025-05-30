using System.Text.Json.Serialization.Metadata;
using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Models;
using Razdor.Communities.Api;
using Razdor.Communities.Infrastructure;
using Razdor.Communities.Services.Authorization;
using Razdor.Identity.Api;
using Razdor.Identity.Api.AuthenticationScheme;
using Razdor.Identity.Api.Routes;
using Razdor.Identity.Infrastructure;
using Razdor.ServiceDefaults;
using Razdor.Shared.Api;
using Razdor.Shared.Api.Constraints;
using Razdor.Shared.Api.OpenAPI;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.RequestSenderContext;
using Razdor.Signaling.Routing;
using Razdor.Signaling.Services;

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
});

// Mediator
builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
    options.PipelineBehaviors =
    [
        typeof(AuthorizationHandler<,>),
        typeof(CommunityPermissionsHandler<,>)
    ];
});

//Cache
builder.Services.AddHybridCache();

// SignalR
builder.Services.AddSignalR();

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

string communityDb = builder.Configuration.GetConnectionString("communitydb")!;
builder.Services.AddCommunityServices(
    new CommunitiesOptions(
        communityDb,
        "communitydb"
    )
);

// SignalingServices
builder.Services.AddSignalingServices(
    builder.Configuration.GetValue<string>(
        "ASPNETCORE_URLS"
    ) + "/signaling"
);

WebApplication app = builder.Build();

app.UseCors();
// Map OpenApi and Swagger UI
app.MapOpenApi("/api/swagger/{documentName}/swagger.json");

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "main-docs");
    options.RoutePrefix = "api/swagger";
});

app.UseCustomNotAuthorizedResponse();
app.UseAuthentication();
app.UseAuthorization();

app.UseRazdorExceptionHandlerMiddleware();

app.MapIdentityApi();
app.MapCommunitiesApi();
app.MapSignalingHub();

app.UseNonExistentRouteResponse();

app.Run();