using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Razdor.Communities.Api;
using Razdor.Communities.Services;
using Razdor.Identity.Api;
using Razdor.Shared.Features;
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

// Swagger UI configuration
builder.Services.Configure<RouteOptions>(
    options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex")
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

// Identity services
builder.AddIdentityServices();

// Community Services
builder.Services.AddKernelServices();
builder.Services.AddSignalingServices(
    builder.Configuration.GetValue<string>(
        "ASPNETCORE_URLS"
    ) + "/signaling"
);


var app = builder.Build();

// Map OpenApi and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(); 

// Identity
app.MapIdentityApi();

app.MapSignalingHub();
//Роуты приложения
app.MapRazdorApi();


app.Run();