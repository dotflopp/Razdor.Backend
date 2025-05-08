using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Models;
using Razdor.Communities.Api;
using Razdor.Identity.Api.AuthenticationScheme;
using Razdor.Identity.Api.Routes;
using Razdor.Identity.Infrastructure;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Shared.Api;
using Razdor.Shared.Api.Constraints;
using Razdor.Shared.Module.RequestSenderContext;
using Razdor.Signaling.Routing;
using Razdor.Signaling.Services;

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
// Swagger UI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    OpenApiSecurityScheme scheme = new();
    scheme.Name = "Authorization";
    scheme.In = ParameterLocation.Header;
    scheme.Type = SecuritySchemeType.ApiKey;
    scheme.BearerFormat = "JWT";
    scheme.Scheme = "oauth2";

    options.AddSecurityDefinition(scheme.Scheme, scheme);

    OpenApiReference reference = new();
    reference.Type = ReferenceType.SecurityScheme;
    reference.Id = scheme.Scheme;
    
    scheme.Reference = reference;
    
    OpenApiSecurityRequirement securityRequirement = new();
    securityRequirement.Add(scheme, []);

    options.AddSecurityRequirement(securityRequirement);
});

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

app.UseCors();

// Map OpenApi and Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomNotAuthorizedResponse();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRazdorExceptionHandlerMiddleware();
app.MapIdentityApi();
app.MapCommunitiesApi();
app.MapSignalingHub();

app.UseNonExistentRouteResponse();

app.Run();