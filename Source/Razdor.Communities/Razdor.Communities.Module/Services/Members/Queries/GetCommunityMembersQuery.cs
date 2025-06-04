using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

public record GetCommunityMembersQuery(
    ulong CommunityId,
    ulong? LastUserId,
    int? UsersCount
): ICommunitiesQuery<IEnumerable<CommunityMemberPreviewModel>>;