using Razdor.Shared.Domain;

namespace Razdor.Messages.Domain;

public record AttachmentMeta(
    ulong Id,
    string FileName,
    string SourceUrl,
    string MediaType,
    long Size
) : MediaFileMeta(FileName, SourceUrl, MediaType, Size)
{
    public AttachmentMeta(ulong id, MediaFileMeta meta) 
        : this(id, meta.FileName, meta.SourceUrl, meta.MediaType, meta.Size)
    {
        
    }
};