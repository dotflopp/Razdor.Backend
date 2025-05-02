namespace Razdor.Identity.Infrastructure;

public record IdentityModuleOptions(
    DateTime AccessTokenStartDate,
    byte[] AccessTokenSecurityKey,
    string SqlConnectionString
);