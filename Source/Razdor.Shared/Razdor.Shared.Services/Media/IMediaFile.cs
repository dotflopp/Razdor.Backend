namespace Razdor.Shared.Module.Media;

public interface IMediaFile
{
    string FileName { get; }
    string ContentType { get; }
    Stream Stream { get; }
}