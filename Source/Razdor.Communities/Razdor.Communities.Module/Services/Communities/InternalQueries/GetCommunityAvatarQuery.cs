using Razdor.Communities.Domain;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.Media;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public record GetCommunityAvatarQuery(
    ulong CommunityId
): ICommunitiesQuery<MediaFile>;