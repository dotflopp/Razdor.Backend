using System.Text.Json.Serialization;

namespace Razdor.Identity.Domain.Users;

[JsonConverter(typeof(JsonStringEnumConverter<DisplayedCommunicationStatus>))]
public enum DisplayedCommunicationStatus
{
    Online,
    Offline,
    DoNotDisturb
}