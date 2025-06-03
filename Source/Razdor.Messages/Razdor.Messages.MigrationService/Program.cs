using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Messages.Infrastructure.DataAccess;
using Razdor.Messages.Module.DataAccess;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

string fakeConnectionString = "mongodb://admin:admin@localhost:27017/messages?authSource=admin&authMechanism=SCRAM-SHA-256";
builder.Services.AddDbContext<MessagesDbContext, MessagesMongoDbContext>(options =>
    options.UseMongoDB(fakeConnectionString,"messages")
);

IHost host = builder.Build();
host.Run();