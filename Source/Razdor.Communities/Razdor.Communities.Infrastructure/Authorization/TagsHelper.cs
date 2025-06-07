namespace Razdor.Communities.Infrastructure.Authorization;

internal static class TagsHelper
{
    public static string ChannelPermissionsKey(ulong channelId, ulong userId)
        => string.Join("-", "channel-permissions", channelId, userId);
    
    public static string CommunityPermissionsKey(ulong communityId, ulong userId) 
        => string.Join("-", "community-permissions", communityId, userId);

    public static string ChannelTag(ulong channelId) 
        => string.Join("-", "channel", channelId);
    
    public static string UserTag(ulong userId)
        => string.Join("-", "user", userId);
    
    public static string MemberTag(ulong communityId, ulong userId)
        => string.Join("-", "member", communityId, userId);
}