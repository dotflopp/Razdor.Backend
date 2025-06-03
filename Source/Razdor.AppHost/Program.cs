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
IResourceBuilder<MongoDBDatabaseResource> messagesDb = mongo.AddDatabase("messagesdb");


IResourceBuilder<ProjectResource> identityMigrations = builder.AddProject<Razdor_Identity_MigrationService>("identity-migrations")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.AddProject<Razdor_StartApp>("razdor")
    .WithReference(identityDb)
    .WithReference(communityDb)
    .WithReference(messagesDb)
    .WaitFor(identityDb)
    .WaitFor(identityMigrations)
    .WaitFor(communityDb)
    .WaitFor(messagesDb)
    .WithExternalHttpEndpoints();

DistributedApplication app = builder.Build();
app.Run();