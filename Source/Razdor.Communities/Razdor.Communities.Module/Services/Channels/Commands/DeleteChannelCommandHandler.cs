using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.DataAccess;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public class DeleteChannelCommandHandler(
    ICommunityChannelsRepository repository,
    CommunitiesDbContext context
) : ICommandHandler<DeleteChannelCommand>
{
    public async ValueTask<Unit> Handle(DeleteChannelCommand command, CancellationToken cancellationToken)
    {
        CommunityChannel channel = await repository.FindAsync(command.ChannelId, cancellationToken);
        List<CommunityChannel> children = await repository.GetChildsAsync(command.ChannelId, cancellationToken);
        
        foreach (CommunityChannel child in children)
        {
            if (child is not ForkChannel fork)
            {
                repository.Delete(child);
                continue;
            }
            
            List<Overwrite> inheritedOverwrites = channel.Overwrites.Select(
                x => new Overwrite(x.TargetId, x.TargetType, new(x.Permissions.Allow, x.Permissions.Deny))
            ).ToList();
            
            child.RemoveParent(inheritedOverwrites);
        }

        repository.Delete(channel);
        await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
