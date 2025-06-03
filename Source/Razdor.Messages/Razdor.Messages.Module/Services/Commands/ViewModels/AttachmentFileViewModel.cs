namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record AttachmentFileViewModel(
    string Name,
    string FileName,  
    string MediaType,
    Stream Stream
);