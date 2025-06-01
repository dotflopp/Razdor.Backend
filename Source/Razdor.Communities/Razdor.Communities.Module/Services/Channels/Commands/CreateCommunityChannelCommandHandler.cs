using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public sealed class CreateCommunityChannelCommandHandler(
    ICommunityChannelsRepository repository,
    SnowflakeGenerator snowflake
) : ICommandHandler<CreateCommunityChannelCommand, ChannelViewModel>
{
    public async ValueTask<ChannelViewModel> Handle(CreateCommunityChannelCommand command, CancellationToken cancellationToken)
    {
        CommunityChannel channel = command.Create(snowflake.Next());
        repository.Add(channel);
        await repository.UnitOfWork.SaveEntitiesAsync();
        return ChannelViewModel.From(channel);
    }
}