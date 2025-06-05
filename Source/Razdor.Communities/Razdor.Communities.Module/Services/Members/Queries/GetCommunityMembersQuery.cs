using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members.Queries;

public record GetCommunityMembersQuery(
    ulong CommunityId,
    ulong? LastUserId,
    int? UsersCount
): ICommunitiesQuery<IEnumerable<CommunityMemberPreviewModel>>;