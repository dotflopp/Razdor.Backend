namespace Razdor.Identity.Module.Services.Users.Avatars;

public record struct AvatarPath(
    ulong UserId
)
{
    public string AsString()
    {
        return $"/api/users/{UserId}/avatar";
    }
};