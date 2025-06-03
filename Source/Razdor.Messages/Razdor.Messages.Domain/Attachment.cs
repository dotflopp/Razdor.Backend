using Razdor.Shared.Domain;

namespace Razdor.Messages.Domain;

public record Attachment(
    ulong Id,
    string FileName,
    string SourceUrl,
    string MediaType, 
    long Size
) : IEntity<ulong>;