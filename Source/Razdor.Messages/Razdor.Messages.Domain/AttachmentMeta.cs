using Razdor.Shared.Domain;

namespace Razdor.Messages.Domain;

public record AttachmentMeta(
    ulong Id,
    string FileName,
    string SourceUrl,
    string MediaType, 
    long Size
) : MediaFileMeta(FileName, SourceUrl, MediaType, Size);