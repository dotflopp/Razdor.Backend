using Razdor.Shared.Module;

namespace Razdor.Messages.Module;

public record struct AttachmentPath(
    ulong ChannelId,
    ulong MessageId,
    ulong AttachmentId
)
{
    public string AsString()
    {
        return $"/attachments/{ChannelId}/{MessageId}/{AttachmentId}";
    }
};

public class AttachmentsStore(IFileStore fileStore)
{
    public async Task<bool> UploadFileAsync(
        AttachmentPath path, 
        string contentType,
        Stream fileStream, 
        CancellationToken cancellationToken = default
    ){  
        return await fileStore.UploadFileAsync(
            path.AsString(),
            contentType, 
            fileStream
        );
    }

    public async Task<Stream> GetFileStreamAsync(
        AttachmentPath path, 
        CancellationToken cancellationToken = default
    ){
        return await fileStore.GetFileStreamAsync(path.AsString(), cancellationToken);
    }
}