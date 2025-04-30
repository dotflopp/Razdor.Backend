using Razdor.Shared.Extensions;

namespace Razdor.Identity.Module.Auth.InternalCommands.ViewModels;

public abstract class AccessTokenValidationError(
    ulong code,
    string message
) : BaseError(code, message) 
{
    public static readonly AccessTokenExpiredError ValidationExpiredTokenValidationError = new AccessTokenExpiredError(
        "The access token has expired."
    );
    public static readonly InvalidAccessTokenError InvalidTokenError = new InvalidAccessTokenError(
        "Invalid access token."
    );
};

public class InvalidAccessTokenError(
    string message    
): AccessTokenValidationError(4, message);

public class AccessTokenExpiredError(
    string message
) : AccessTokenValidationError(5, message);
