using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Members;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Members.ViewModels;

public record CommunityMemberPreviewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId, 
    string Nickname, 
    string? Avatar, 
    DateTimeOffset JoiningDate, 
    VoiceState VoiceState,
    IEnumerable<string> RoleIds
){
    public static CommunityMemberPreviewModel From(CommunityMember member, MemberProfileViewModel profile)
    {
        ArgumentNullException.ThrowIfNull(profile.IdentityName, nameof(profile.IdentityName));
        ArgumentNullException.ThrowIfNull(profile.Nickname, nameof(profile.Nickname));
        
        return new CommunityMemberPreviewModel(
            member.UserId,
            member.CommunityId,
            profile.Nickname,
            profile.Avatar,
            member.JoiningDate,
            member.VoiceState,
            member.RoleIds.Select(x => x.ToString())
        );
    }
}