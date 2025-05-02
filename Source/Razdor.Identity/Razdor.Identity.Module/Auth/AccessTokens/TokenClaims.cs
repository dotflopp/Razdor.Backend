namespace Razdor.Identity.Module.Auth.AccessTokens;

public record TokenClaims(
    ulong UserId,
    DateTimeOffset CreationTime
);