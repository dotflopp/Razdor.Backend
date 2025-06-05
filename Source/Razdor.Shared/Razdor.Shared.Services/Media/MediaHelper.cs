using System.Windows.Input;
using Mediator;
using Razdor.Shared.Domain;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media.Exceptions;

namespace Razdor.Shared.Module.Media;

public static class MediaHelper
{
    public static async Task<MediaFile> GetMediaFileAsync<TPath>(
        this IFileStore store,
        TPath path,
        MediaFileMeta meta,
        CancellationToken cancellationToken = default
    ) where TPath : IMediaContentPath
    {
        Stream stream = await store.GetFileStreamAsync(
            path.AsString(),
            cancellationToken
        );
        
        if (stream == Stream.Null)
            MediaFileNotFoundException.Throw();

        return new MediaFile(
            meta.FileName,
            meta.MediaType,
            stream
        );
    }

    public static async Task<MediaFileMeta> UploadMediaFileAsync<TPath>(
        this IFileStore store,
        TPath path,
        IMediaFile file,
        CancellationToken cancellationToken = default
    ) where TPath : IMediaContentPath
    {
        string strPath = path.AsString(); 
        bool isStored = await store.UploadFileAsync(
            strPath,
            file.ContentType,
            file.Stream,
            cancellationToken
        );
         
        if (!isStored)
            MediaFileNotUploadedException.Throw();
        
        return new MediaFileMeta(
            file.FileName,
            strPath,
            file.ContentType,
            file.Stream.Length
        );
    }
}