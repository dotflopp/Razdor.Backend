// See https://aka.ms/new-console-template for more information

using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Identity.Infrastructure.DataAccess.PostgreSQL;
using Razdor.Identity.Migrations;
using Razdor.Identity.Module.DataAccess;
using Razdor.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => 
        tracing.AddSource(Worker<IdentityDbContext>.ActivitySourceName)
    );

builder.Services.AddDbContext<IdentityDbContext, IdentityPostgreSqlContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("identitydb"));
});


builder.Services.AddHostedService<Worker<IdentityPostgreSqlContext>>();

var host = builder.Build();
host.Run();