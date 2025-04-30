namespace Razdor.Identity.Module.Auth.AccessTokens;

public record AccessTokenClaims(
    ulong UserId,
    DateTimeOffset CreationTime
);