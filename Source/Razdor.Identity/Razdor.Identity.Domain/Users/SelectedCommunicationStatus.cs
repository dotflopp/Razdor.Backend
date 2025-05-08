using System.Text.Json.Serialization;

namespace Razdor.Identity.Domain.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SelectedCommunicationStatus
{
    Online,
    Invisible,
    DoNotDisturb
}