using Mediator;
using Microsoft.Extensions.Logging;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public class GetChannelMemberPermissionsHandler(
    ICommunityChannelsRepository channels,
    ICommunityMembersRepository members,
    ICommunityPermissionsAccessor communityPermissions,
    IChannelPermissionsAccessor channelPermissions,
    ILogger<GetChannelMemberPermissionsHandler> logger
) : IQueryHandler<GetChannelMemberPermissions, UserPermissions>
{
    public async ValueTask<UserPermissions> Handle(GetChannelMemberPermissions query, CancellationToken cancellationToken)
    {
        CommunityChannel channel = await channels.FindAsync(query.ChannelId, cancellationToken);
        CommunityMember member = await members.FindAsync(channel.CommunityId, query.UserId, cancellationToken);
        
        bool hasParentPermissions = false;
        UserPermissions inheritedPermissions = UserPermissions.None;
        
        if (channel.IsSyncing)
        {
            try
            {
                inheritedPermissions = await channelPermissions.GetMemberPermissionsAsync(
                    query.UserId, channel.ParentId, cancellationToken
                );
                hasParentPermissions = true;
            }
            catch (ResourceNotFoundException ex) when (ex.ResourceType.IsAssignableTo(typeof(CommunityChannel)))
            {
                logger.LogError(ex, $"Parent channel by id {channel.ParentId} not found for channel {channel.Id}");       
            }
        }
        
        if (!hasParentPermissions)
        {
            inheritedPermissions = await communityPermissions.GetMemberPermissionsAsync(
                channel.CommunityId, query.UserId, cancellationToken
            );
        }
        
        return channel.GetPermissionsWithOverwrites(member, inheritedPermissions);
    }
}