using System.Text.Json.Serialization;

namespace Razdor.Shared.Domain.Exceptions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCode : ulong
{
    Unauthorized,
    AccessForbidden,

    //Identitiy
    InvalidPasswordOrEmail,
    IdentityNameOrEmailAlreadyExists,
    UserNotFound,

    //AccessToken Validation
    InvalidAccessToken,
    AccessTokenExpired,

    //Communities
    CommunityMemberNotFound,
    CommunityNotFound,
    InviteNotFound,
    ReJoiningToCommunity,

    //Routing
    NonExistentRoute
}