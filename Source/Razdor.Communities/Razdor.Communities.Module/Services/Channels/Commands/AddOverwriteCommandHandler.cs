using System.Collections.ObjectModel;
using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public class AddOverwriteCommandHandler(
    ICommunityChannelsRepository channels,
    ICommunitiesRepository communities,
    IRequestSenderContext sender,
    IChannelPermissionsAccessor channelPermissions,
    ICommunityPermissionsAccessor communityPermissions
) : ICommandHandler<AddOverwriteCommand>
{
    public async ValueTask<Unit> Handle(AddOverwriteCommand command, CancellationToken cancellationToken)
    {
        CommunityChannel channel = await channels.FindAsync(command.ChannelId, cancellationToken);

        if (channel is not OverwritesPermissionChannel overwritesChannel)
            throw new InvalidChannelOperationException($"{channel.Type} not supported");

        UserPermissions senderPermissions = await channelPermissions.GetMemberPermissionsAsync(sender.User.Id, channel.Id, cancellationToken);
        (_, uint senderPriority) = await communityPermissions.GetMemberPermissionsAndPriorityAsync(channel.CommunityId, sender.User.Id, cancellationToken);

        if (command.Overwrite.TargetType == PermissionTargetType.Role)
        {
            Community community = await communities.FindAsync(channel.CommunityId, cancellationToken);
            Role? role = community.FindRoleById(command.Overwrite.TargetId);
            ResourceNotFoundException.ThrowIfNull(role, command.Overwrite.TargetId);
            
            if (role.Priority < senderPriority)
                NotEnoughRightsException.ThrowResourceHasHigherPriority();
        }
        
        List<Overwrite>? inheritedOverwrites = null;
        if (overwritesChannel.IsSyncing)
            inheritedOverwrites = await GetParentOverwritesAsync(overwritesChannel.ParentId, cancellationToken);
        
        Overwrite? overwrite = overwritesChannel.RemoveOverwrite(command.Overwrite.TargetId, inheritedOverwrites);

        // Пользователь сможет поменять только те разрешения, которые имеет сам
        UserPermissions allow = command.Overwrite.Permissions.Allow & senderPermissions;
        UserPermissions deny = command.Overwrite.Permissions.Deny & senderPermissions;
        
        if (overwrite != null)
        { // Поэтому если есть, то сохраняем старые разрешения, которые пользователь не может менять
            allow |= overwrite.Permissions.Allow & ~senderPermissions;
            deny |= overwrite.Permissions.Deny & ~senderPermissions;
        }
        
        overwritesChannel.SetOverwrite(
            command.Overwrite.TargetId,
            new OverwritePermissions(allow, deny),
            command.Overwrite.TargetType,
            inheritedOverwrites
        );

        await channels.UnitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }

    private async Task<List<Overwrite>> GetParentOverwritesAsync(ulong parentId, CancellationToken cancellationToken = default)
    {
        CommunityChannel channel = await channels.FindAsync(parentId, cancellationToken);
        while (channel.IsSyncing)
        {
            channel = await channels.FindAsync(channel.Id, cancellationToken);
            if (channel.ParentId == parentId)
                throw new InvalidOperationException($"Parent channel {parentId} depends on child channel");
        }
        
        return channel.Overwrites.ToList();
    }
}