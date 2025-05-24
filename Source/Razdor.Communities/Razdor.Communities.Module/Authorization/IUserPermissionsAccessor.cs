using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Services.Authorization;

public interface IUserPermissionsAccessor
{
    Task<UserPermissions> GetUserCommunityPermissionsAsync(ulong communityId, ulong userId);
}