using System.Windows.Input;
using Mediator;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Domain;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Shared.Module.Media;

public static class MediaHelper
{
    public static async Task<MediaFileViewModel> GetMediaFileAsync<TPath>(
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

        return new MediaFileViewModel(
            meta.FileName,
            meta.MediaType,
            stream
        );
    }
}