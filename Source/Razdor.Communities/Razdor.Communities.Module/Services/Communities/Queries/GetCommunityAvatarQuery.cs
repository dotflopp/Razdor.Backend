using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.Media;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public record GetCommunityAvatarQuery(
    ulong CommunityId
): ICommunitiesQuery<MediaFile>;