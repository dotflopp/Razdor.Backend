using Razdor.Communities.Domain.Permissions;

namespace Razdor.Shared.Module.Authorization;

public interface IRequiredCommunityPermissions : IAuthorizationRequiredMessage
{
    ulong CommunityId { get; }
    UserPermissions RequiredPermissions { get; }
}