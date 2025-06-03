namespace Razdor.Shared.Domain;

public record MediaFileMeta(
    string FileName,
    string SourceUrl,
    string MediaType,
    long Size
);