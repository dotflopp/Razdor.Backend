namespace Razdor.Api.Multipart;

public record MultipartRequestFile(
    string Name,
    string Filename,
    string MediaType,
    Stream Stream
);