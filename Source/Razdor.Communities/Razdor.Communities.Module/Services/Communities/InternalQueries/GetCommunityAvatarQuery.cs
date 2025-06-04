using Razdor.Communities.Domain;
using Razdor.Communities.Module.Contracts;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public record GetCommunityAvatarQuery(
    ulong CommunityId
): ICommunitiesQuery<MediaFile>;