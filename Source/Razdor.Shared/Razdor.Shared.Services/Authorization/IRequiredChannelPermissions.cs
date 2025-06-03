using Razdor.Communities.Domain.Permissions;

namespace Razdor.Shared.Module.Authorization;

public interface IRequiredChannelPermissions : IAuthorizationRequiredMessage
{
    ulong ChannelId { get; }
    UserPermissions RequiredPermissions { get; }
}