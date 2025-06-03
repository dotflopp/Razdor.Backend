using System.Text.Json.Serialization;
using Razdor.Messages.Domain;
using Razdor.Shared.Module;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record AttachmentViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    string SourceUrl,
    string MediaType,
    int Size
){
    public static AttachmentViewModel From(Attachment arg)
    {
        return new AttachmentViewModel(arg.Id, arg.SourceUrl, arg.MediaType, arg.Size);
    }
}