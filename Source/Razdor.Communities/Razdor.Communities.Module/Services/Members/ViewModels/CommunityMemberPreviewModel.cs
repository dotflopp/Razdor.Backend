using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Module.Services.Members.ViewModels;

public record CommunityMemberPreviewModel(
    ulong UserId, 
    ulong CommunityId, 
    string Nickname, 
    string Avatar, 
    DateTimeOffset JoiningDate, 
    VoiceState VoiceState, 
    IReadOnlyCollection<ulong> RoleIds
){
    public CommunityMemberPreviewModel From(CommunityMember member, MemberProfile profile)
    {
        ArgumentNullException.ThrowIfNull(profile.Nickname, nameof(profile.Nickname));
        ArgumentNullException.ThrowIfNull(profile.Avatar, nameof(profile.Avatar));
        
        return new CommunityMemberPreviewModel(
            member.UserId,
            member.CommunityId,
            profile.Nickname,
            profile.Avatar.SourceUrl,
            member.JoiningDate,
            member.VoiceState,
            member.RoleIds
        );
    }
}