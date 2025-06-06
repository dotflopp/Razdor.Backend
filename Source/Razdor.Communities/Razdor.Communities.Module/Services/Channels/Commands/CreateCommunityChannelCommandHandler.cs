using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public sealed class CreateCommunityChannelCommandHandler(
    ICommunityChannelsRepository repository,
    SnowflakeGenerator snowflake
) : ICommandHandler<CreateCommunityChannelCommand, ChannelViewModel>
{
    public async ValueTask<ChannelViewModel> Handle(CreateCommunityChannelCommand command, CancellationToken cancellationToken)
    {
        CommunityChannel? parent = null;
        if (command.ParentId != 0)
            parent = await repository.FindAsync(command.ParentId, cancellationToken);
        
        CommunityChannel channel = command.Create(snowflake.Next(), parent);
        repository.Add(channel);
        await repository.UnitOfWork.SaveEntitiesAsync();
        
        return ChannelViewModel.From(channel);
    }
}