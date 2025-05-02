using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Razdor.Communities.Api;
using Razdor.Identity.Api.AuthenticationScheme;
using Razdor.Identity.Api.Routes;
using Razdor.Identity.Infrastructure;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Shared.Api;
using Razdor.Shared.Module.RequestSenderContext;
using Razdor.Signaling.Routing;
using Razdor.Signaling.Services;
using Razdor.StartApp.Constraints;

var builder = WebApplication.CreateSlimBuilder(args);

// Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(0, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});

// CORS
builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(policy =>
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

// Swagger UI configuration
builder.Services.Configure<RouteOptions>(options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex")
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mediator
builder.Services.AddMediator(options =>
    options.ServiceLifetime = ServiceLifetime.Transient
);

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

// Snowflake Generator
builder.Services.AddSingleton(
    new SnowflakeGenerator(0, new DateTime(2025, 1, 1))
);

//UserContext Accessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IRequestSenderContext, RequestSenderContextAccessor>();

// Identity services
builder.Services.AddIdentityServices(
    new IdentityModuleOptions(
        new DateTime(2025, 1, 1),
        Convert.FromBase64String(
            "K3UA5ta52VOeTguHAgYaw+5IV4KLUlflzx3sYjy8WpnLPsmR8oYsIHewP4U7cE/JBNRR9gNdGhaflBlJcGXA6lEu8ZdL1+x9muyI1nfuivA="
        ),
        builder.Configuration.GetConnectionString("LocalIdentity") ?? throw new NullReferenceException()
    )
);

// SignalingServices
builder.Services.AddSignalingServices(
    builder.Configuration.GetValue<string>(
        "ASPNETCORE_URLS"
    ) + "/signaling"
);


var app = builder.Build();

app.UseRazdorExceptionHandlerMiddleware();

// Map OpenApi and Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Identity
app.MapIdentityApi();

app.MapSignalingHub();
//Роуты приложения
app.MapCommunitiesApi();


app.Run();