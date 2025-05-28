using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Services.Communities.Queries;

public record class GetCommunityQuery(ulong CommunityId) :
    ICommunitiesQuery<CommunityViewModel>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}