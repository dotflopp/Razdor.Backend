using Razdor.Shared.Module.Media;

namespace Razdor.Identity.Module.Services.Users;

public record struct UserAvatarPath(
    ulong UserId
) : IMediaContentPath
{
    public string AsString()
    {
        return $"/api/users/{UserId}/avatar";
    }
};