namespace Razdor.Api.Multipart;

public record ContentWithFiles<TMainContent>(
    TMainContent Conetent,
    IAsyncEnumerable<MultipartRequestFile> Files
)
{
}