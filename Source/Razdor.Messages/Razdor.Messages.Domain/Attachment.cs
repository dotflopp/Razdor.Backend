using Razdor.Shared.Domain;

namespace Razdor.Messages.Domain;

public record Attachment(
    ulong Id,
    string SourceUrl,
    string MediaType,
    int Size
) : IEntity<ulong>;