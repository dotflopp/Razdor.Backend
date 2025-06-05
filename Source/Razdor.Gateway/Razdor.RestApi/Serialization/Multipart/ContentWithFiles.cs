namespace Razdor.RestApi.Multipart;

public record ContentWithFiles<TMainContent>(
    TMainContent Conetent,
    IAsyncEnumerable<MultipartRequestFile> Files
)
{
}