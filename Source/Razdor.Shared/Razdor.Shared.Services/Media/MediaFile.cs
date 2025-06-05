namespace Razdor.Shared.Module.Media;

public record MediaFile(
    string FileName,
    string ContentType,
    Stream Stream
): IMediaFile;