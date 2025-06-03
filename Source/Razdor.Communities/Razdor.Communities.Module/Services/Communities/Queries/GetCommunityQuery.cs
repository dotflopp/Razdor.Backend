using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public record class GetCommunityQuery(ulong CommunityId) :
    ICommunitiesQuery<CommunityViewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}