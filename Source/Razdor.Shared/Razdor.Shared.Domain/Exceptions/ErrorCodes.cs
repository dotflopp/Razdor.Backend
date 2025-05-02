namespace Razdor.Shared.Domain.Exceptions;

public enum ErrorCodes : ulong
{
    //Auth
    Unauthenticated,
    Unauthorized,

    //Authentication
    InvalidPasswordOrEmail,
    UserAlreadyExists,

    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired,

    //Not Found errors
    UserNotFound,

}