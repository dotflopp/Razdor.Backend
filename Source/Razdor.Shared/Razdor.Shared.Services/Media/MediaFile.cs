using Razdor.Shared.Module.Media;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MediaFile(
    string FileName,
    string ContentType,
    Stream Stream
): IMediaFile;