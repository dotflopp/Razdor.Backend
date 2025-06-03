using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Identity.Module.Services.Users.Commands;

public record UploadUserAvatarCommand(
    string FileName,
    string ContentType,
    Stream AvatarStream
): IIdentityCommand, IAuthorizationRequiredMessage;