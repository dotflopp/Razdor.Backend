using Razdor.Communities.Domain.Users;

namespace Razdor.DataAccess.EntityFramework.Entities;

public class User : IUser
{
    public required ulong Id { get; init; }
    public required string Username { get; init; }
    public required ushort Discriminator { get; init; }
    public required string? AvatarUrl { get; init; }
    public required string? Email { get; init; }

    internal IReadOnlyCollection<Guild>? Guilds { get; set; }
}