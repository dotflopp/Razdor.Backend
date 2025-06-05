using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Module.Media;

namespace Razdor.Identity.Module.Services.Users.Queries;

public record GetUserAvatarQuery(
    ulong UserId
) : IIdentityCommand<MediaFile>;