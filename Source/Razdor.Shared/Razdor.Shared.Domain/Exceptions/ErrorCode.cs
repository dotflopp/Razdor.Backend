using System.Text.Json.Serialization;

namespace Razdor.Shared.Domain.Exceptions;

[JsonConverter(typeof(JsonStringEnumConverter<ErrorCode>))]
public enum ErrorCode : ulong
{
    Unauthorized,
    AccessForbidden,
    InvalidOperationException,
    
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
    ChannelNotFound,
    InviteNotFound,
    ReJoiningToCommunity,

    //Media
    InvalidMediaType,
    MediaNotFound,
    MediaNotUploaded,
    
    //Routing
    NonExistentRoute,
}