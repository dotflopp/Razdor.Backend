using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Authorization;

public interface IRequiredCommunityPermissions : IAuthorizationRequiredMessage
{
    ulong CommunityId { get; }
    UserPermissions RequiredPermissions { get; }
}