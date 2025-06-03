using System.Text.Json.Serialization;

namespace Razdor.Communities.Domain;

[JsonConverter(typeof(JsonStringEnumConverter<CommunityNotificationPolicy>))]
public enum CommunityNotificationPolicy
{
    All,
    OnlyMentions,
    Nothing
}