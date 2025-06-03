using System.Text.Json.Serialization;

namespace Razdor.Communities.Domain.Permissions;

[JsonConverter(typeof(JsonStringEnumConverter<PermissionTargetType>))]
public enum PermissionTargetType
{
    User,
    Role
}