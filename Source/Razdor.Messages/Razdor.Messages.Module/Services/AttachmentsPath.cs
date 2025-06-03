namespace Razdor.Messages.Module.Services;

public record struct AttachmentPath(
    ulong ChannelId,
    ulong MessageId,
    ulong AttachmentId
)
{
    public string AsString()
    {
        return $"/api/attachments/{ChannelId}/{MessageId}/{AttachmentId}";
    }
};