using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.ViewModels;

namespace Razdor.Communities.Services.Services.Communities.Queries;

public record class GetCommunityQuery(ulong CommunityId) :
    ICommunitiesQuery<CommunityViewModel>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}