using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Members;

namespace Razdor.Communities.Module.Services.Members.Queries;

public record GetCommunityMembersQuery(
    ulong CommunityId,
    ulong? LastUserId,
    int? UsersCount
): ICommunitiesQuery<IEnumerable<CommunityMemberPreviewModel>>;