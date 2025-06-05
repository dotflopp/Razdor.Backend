using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Channels.ViewModels;

public record OverwriteViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong TargetId,
    PermissionTargetType TargetType,
    OverwritePermissions Permissions
)
{
    public static OverwriteViewModel From(Overwrite overwrite)
    {
        return new OverwriteViewModel(overwrite.TargetId, overwrite.TargetType, overwrite.Permissions);
    }
}