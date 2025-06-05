using System.Text.Json.Serialization;

namespace Razdor.Shared.Domain.Exceptions;

[JsonConverter(typeof(JsonStringEnumConverter<ErrorCode>))]
public enum ErrorCode : ulong
{
    Unauthorized,
    AccessForbidden,
    NotEnoughRights,
    InvalidOperationException,
    BadRequest,
    NotFound,
    
    //Identitiy
    InvalidPasswordOrEmail,
    IdentityNameOrEmailAlreadyExists,
    UserNotFound,

    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired,

    //Communities
    ReJoiningToCommunity,

    //Media
    InvalidMediaType,
    MediaNotFound,
    MediaNotUploaded,
    
    //Routing
    NonExistentRoute,
}