// See https://aka.ms/new-console-template for more information

using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Identity.Infrastructure.DataAccess.Sql;
using Razdor.Identity.Migrations;
using Razdor.Identity.Module.DataAccess;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMediator(options =>
    options.ServiceLifetime = ServiceLifetime.Transient
);

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => 
        tracing.AddSource(Worker<IdentityPostgreSQLContext>.ActivitySourceName)
    );

builder.Services.AddDbContext<IIdentityDbContext, IdentityPostgreSQLContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("identitydb"));
});


builder.Services.AddHostedService<Worker<IdentityPostgreSQLContext>>();

var host = builder.Build();
host.Run();