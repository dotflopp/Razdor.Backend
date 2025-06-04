using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Media;

namespace Razdor.Identity.Module.Services.Users.Commands;

public record UploadUserAvatarCommand(
    string FileName,
    string ContentType,
    Stream Stream
): IIdentityCommand, IAuthorizationRequiredMessage, IMediaFile;