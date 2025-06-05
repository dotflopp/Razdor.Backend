namespace Razdor.RestApi.Multipart;

public record MultipartRequestFile(
    string Name,
    string Filename,
    string MediaType,
    Stream Stream
);