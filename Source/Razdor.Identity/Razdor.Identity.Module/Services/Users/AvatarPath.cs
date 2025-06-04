using Razdor.Shared.Module.Media;

namespace Razdor.Identity.Module.Services.Users.Avatars;

public record struct AvatarPath(
    ulong UserId
) : IMediaContentPath
{
    public string AsString()
    {
        return $"/api/users/{UserId}/avatar";
    }
};