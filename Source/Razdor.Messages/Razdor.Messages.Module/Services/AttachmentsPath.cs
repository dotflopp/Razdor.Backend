using Razdor.Shared.Module.Media;

namespace Razdor.Messages.Module.Services;

public record struct AttachmentPath(
    ulong ChannelId,
    ulong MessageId,
    ulong AttachmentId
) : IMediaContentPath
{
    public string AsString()
    {
        return $"/api/attachments/{ChannelId}/{MessageId}/{AttachmentId}";
    }
};