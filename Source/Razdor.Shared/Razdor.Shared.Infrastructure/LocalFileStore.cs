using Razdor.Shared.Module.Media;

namespace Razdor.Shared.Infrastructure;

public class LocalFileStore : IFileStore
{
    public async Task<bool> UploadFileAsync(string fileName, string fileContentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        try
        {
            Directory.CreateDirectory(
                Path.GetDirectoryName(fileName)
            );
            using FileStream file = File.Create(fileName);
            await fileStream.CopyToAsync(file);
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }
    
    public Task<Stream> GetFileStreamAsync(string fileName, CancellationToken cancellationToken = default)
    {
        Stream stream;
        try
        {
            stream = File.OpenRead(fileName);
        }
        catch (IOException)
        {
            stream = Stream.Null;
        }
        return Task.FromResult(stream);
    }
    
    public Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(File.Exists(fileName));
    }
}