using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

IResourceBuilder<MongoDBServerResource> mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithMongoExpress();

IResourceBuilder<PostgresDatabaseResource> identityDb = postgres.AddDatabase("identitydb");
IResourceBuilder<MongoDBDatabaseResource> communityDb = mongo.AddDatabase("communitydb");
IResourceBuilder<MongoDBDatabaseResource> messagingDb = mongo.AddDatabase("messagingdb");


IResourceBuilder<ProjectResource> identityMigrations = builder.AddProject<Razdor_Identity_MigrationService>("identity-migrations")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.AddProject<Razdor_StartApp>("razdor")
    .WithReference(identityDb)
    .WithReference(communityDb)
    .WithReference(messagingDb)
    .WaitFor(identityDb)
    .WaitFor(identityMigrations)
    .WaitFor(communityDb)
    .WaitFor(messagingDb)
    .WithExternalHttpEndpoints();

DistributedApplication app = builder.Build();
app.Run();