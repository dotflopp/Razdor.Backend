using System.Text.Json.Serialization;

namespace Razdor.Communities.Module.Services.Members.ViewModels;

[JsonConverter(typeof(JsonStringEnumConverter<CommunicationStatus>))]
public enum CommunicationStatus
{
    Online,
    Offline,
    DoNotDisturb
}