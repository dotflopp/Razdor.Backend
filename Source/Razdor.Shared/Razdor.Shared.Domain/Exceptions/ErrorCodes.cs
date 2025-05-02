namespace Razdor.Shared.Domain.Exceptions;

public enum ErrorCodes
{
    //Auth
    Unauthenticated,
    Unauthorized,

    //Authentication
    InvalidPasswordOrEmail,
    UserAlreadyExists,

    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired
}