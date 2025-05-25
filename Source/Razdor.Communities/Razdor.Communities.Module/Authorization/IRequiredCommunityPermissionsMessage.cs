using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Authorization;

public interface IRequiredCommunityPermissionsMessage: IAuthorizationRequiredMessage
{
    ulong CommunityId { get; }
    UserPermissions RequiredPermissions { get; }
}