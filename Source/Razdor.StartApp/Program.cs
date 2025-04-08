using Asp.Versioning;
using Microsoft.AspNetCore.Routing.Constraints;
using Razdor.Identity.Api;
using Razdor.Shared.Features;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.Configure<RouteOptions>(
    options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex")
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, IdentityJsonSerializerContext.Default);
});

builder.Services.AddMediator(options =>
    options.ServiceLifetime = ServiceLifetime.Transient
);

builder.Services.AddSingleton(
    new SnowflakeGenerator(0, new DateTime(2025, 1, 1))    
);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(0, 1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});

builder.AddIdentityServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); 

app.MapIdentityApi();
app.Run();