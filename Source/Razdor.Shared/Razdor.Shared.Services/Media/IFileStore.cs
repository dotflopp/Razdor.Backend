namespace Razdor.Shared.Module.Media;

public interface IFileStore
{
    public Task<bool> UploadFileAsync(
        string fileName, 
        string fileContentType, 
        Stream fileStream,
        CancellationToken cancellationToken = default
    );
    
    public Task<Stream> GetFileStreamAsync(
        string fileName, 
        CancellationToken cancellationToken = default
    );
    
    public Task<bool> ExistsAsync(
        string fileName, 
        CancellationToken cancellationToken = default
    );
}