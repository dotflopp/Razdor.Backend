namespace Razdor.Signaling.Internal;

public class UserIdentity
{
    public ulong Id { get; init; }
    public required string Nickname { get; init; }
    public required string? AvatarUrl { get; init; }
}