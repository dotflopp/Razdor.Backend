namespace Razdor.Shared.Features;

public record RequesterIdentity(
    ulong Id,
    RequesterKind Type,
    IDictionary<string, string> Items
);

public enum RequesterKind
{
    Unknown,
    User,
    Bot,
    Service
}