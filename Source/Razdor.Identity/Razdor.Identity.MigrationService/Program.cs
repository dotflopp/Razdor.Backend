// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Identity.Infrastructure.DataAccess;
using Razdor.Identity.Migrations;
using Razdor.Identity.Module.DataAccess;
using Razdor.ServiceDefaults;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(Worker<IdentityDbContext>.ActivitySourceName)
    );

builder.Services.AddDbContext<IdentityDbContext, IdentityPostgresDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("identitydb"));
});


builder.Services.AddHostedService<Worker<IdentityPostgresDbContext>>();

IHost host = builder.Build();
host.Run();