using System.Text.Json.Serialization;

namespace Razdor.Communities.Domain.Permissions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PermissionTargetType
{
    User,
    Role
}