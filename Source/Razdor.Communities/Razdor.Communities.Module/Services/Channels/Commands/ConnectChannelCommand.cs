using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public record ConnectChannelCommand(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId
): ICommunitiesCommand<SessionViewModel>, IRequiredChannelPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ViewChannel | UserPermissions.Connect;
}