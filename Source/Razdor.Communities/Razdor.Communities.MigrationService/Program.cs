using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razdor.Communities.Infrastructure.DataAccess;
using Razdor.Communities.Module.DataAccess;

var builder = WebApplication.CreateSlimBuilder(args);

string fakeConnectionString = "mongodb://admin:admin@localhost:27017/communitydb?authSource=admin&authMechanism=SCRAM-SHA-256";
builder.Services.AddDbContext<CommunitiesDbContext, CommunitiesMongoDbContext>(options =>
    options.UseMongoDB(fakeConnectionString,"communitydb")
);

builder.Build().Run();