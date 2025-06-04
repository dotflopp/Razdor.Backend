namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MediaFileViewModel(
    string FileName,
    string ContentType,
    Stream Stream
);