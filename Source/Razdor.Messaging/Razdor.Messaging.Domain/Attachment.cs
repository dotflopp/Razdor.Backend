using System.Net.Mime;
using Razdor.Shared.Domain;

namespace Razdor.Messaging.Domain;

public record Attachment(
    ulong Id,
    string SourceUrl,
    string MediaType,
    int Size
) : IEntity<ulong>;