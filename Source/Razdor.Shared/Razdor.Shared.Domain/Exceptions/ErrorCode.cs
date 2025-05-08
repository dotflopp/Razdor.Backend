namespace Razdor.Shared.Domain.Exceptions;

public enum ErrorCode : ulong
{
    Unauthenticated,
    Unauthorized,

    //Login and Signup
    InvalidPasswordOrEmail,
    IdentityNameAlreadyExists,
    EmailAlreadyExists,
    
    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired,

    //Not Found errors
    UserNotFound,

}