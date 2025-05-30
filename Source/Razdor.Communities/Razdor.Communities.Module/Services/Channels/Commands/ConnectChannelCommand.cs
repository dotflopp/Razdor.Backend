using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Channels.Commands;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public record ConnectChannelCommand(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId
): ICommunitiesCommand<SessionViewModel>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.Connect;
}