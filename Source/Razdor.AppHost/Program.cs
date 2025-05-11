using Microsoft.Extensions.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithMongoExpress();

var identityDb = postgres.AddDatabase("identitydb");
var communityDb = mongo.AddDatabase("communitydb");

var identityMigrations = builder.AddProject<Razdor_Identity_MigrationService>("identity-migrations")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.AddProject<Razdor_StartApp>("razdor")
    .WithReference(identityDb)
    .WithReference(communityDb)
    .WaitFor(identityDb)
    .WaitFor(identityMigrations)
    .WaitFor(communityDb)
    .WithExternalHttpEndpoints();

var app = builder.Build();
app.Run();
