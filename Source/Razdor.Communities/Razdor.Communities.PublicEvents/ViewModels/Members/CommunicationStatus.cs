using System.Text.Json.Serialization;

namespace Razdor.Communities.PublicEvents.ViewModels.Members;

[JsonConverter(typeof(JsonStringEnumConverter<CommunicationStatus>))]
public enum CommunicationStatus
{
    Online,
    Offline,
    DoNotDisturb
}