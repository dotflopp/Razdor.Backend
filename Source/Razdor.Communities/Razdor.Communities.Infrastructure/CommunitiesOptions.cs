namespace Razdor.Communities.Infrastructure;

public record CommunitiesOptions(
    string ConnectionString,
    string DataBaseName
);