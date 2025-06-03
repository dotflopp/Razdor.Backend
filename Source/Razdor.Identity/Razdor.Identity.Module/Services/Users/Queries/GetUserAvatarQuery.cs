using Razdor.Identity.Module.Contracts;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Identity.Module.Services.Users.Avatars.Queries;

public record GetUserAvatarQuery(
    ulong UserId
) : IIdentityCommand<MediaFileViewModel>;