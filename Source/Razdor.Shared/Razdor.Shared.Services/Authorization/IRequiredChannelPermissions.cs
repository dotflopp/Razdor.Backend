using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Authorization;

public interface IRequiredChannelPermissions : IAuthorizationRequiredMessage
{
    ulong ChannelId { get; }
    UserPermissions RequiredPermissions { get; }
}