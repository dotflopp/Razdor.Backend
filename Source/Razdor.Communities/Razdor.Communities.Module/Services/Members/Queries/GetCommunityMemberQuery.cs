using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Members.Queries;

public record GetCommunityMemberQuery(
    ulong CommunityId,
    ulong UserId
): ICommunitiesQuery<CommunityMemberPreviewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}