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
        
        Stack<CommunityChannel> channelFamily = new();
        channelFamily.Push(channel);
        
        while (channel.IsSyncing)
        {
            channel = await channels.FindAsync(channel.ParentId, cancellationToken);
            channelFamily.Push(channel);

            if (channel.ParentId == query.ChannelId)
            {
                string channelIds = string.Join(
                    ",", channelFamily.Select(x => x.Id)
                );
                
                logger.LogWarning($"A closed chain of channel inheritance [{channelIds}]");
                break;
            }
        }

        UserPermissions memberPermissions = await communityPermissions.GetMemberPermissionsAsync(
            channel.CommunityId, query.UserId, cancellationToken
        );
        
        while (channelFamily.Count > 0)
        {
           channel = channelFamily.Pop();
           memberPermissions = channel.GetPermissionsWithOverwrites(member, memberPermissions);
        }
        
        return memberPermissions;
    }
}