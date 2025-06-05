using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Members;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Members.ViewModels;

public record CommunityMemberPreviewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    string IdentityName,
    CommunicationStatus Status,
    string Nickname, 
    string? Avatar, 
    DateTimeOffset JoiningDate, 
    VoiceState? VoiceState,
    IEnumerable<string> RoleIds
){
    public static CommunityMemberPreviewModel From(CommunityMember member, UserDataViewModel userData)
    {
        ArgumentNullException.ThrowIfNull(userData.IdentityName, nameof(userData.IdentityName));
        ArgumentNullException.ThrowIfNull(userData.Nickname, nameof(userData.Nickname));
        
        return new CommunityMemberPreviewModel(
            member.UserId,
            member.CommunityId,
            userData.IdentityName,
            userData.CommunicationStatus,
            userData.Nickname,
            userData.Avatar,
            member.JoiningDate,
            member.VoiceState == VoiceState.Default? null: member.VoiceState,
            member.RoleIds.Select(x => x.ToString())
        );
    }
}