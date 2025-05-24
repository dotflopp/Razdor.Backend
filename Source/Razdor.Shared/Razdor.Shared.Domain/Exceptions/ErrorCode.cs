using System.Text.Json.Serialization;

namespace Razdor.Shared.Domain.Exceptions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCode : ulong
{
    Unauthorized,
    AccessForbidden,

    //Login and Signup
    InvalidPasswordOrEmail,
    IdentityNameAlreadyExists,
    EmailAlreadyExists,
    
    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired,

    //Not Found errors
    UserNotFound,

    NonExistentRoute,
}