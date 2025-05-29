using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Services.DataAccess;
using Razdor.Shared.Module;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Services.Services.Channels.Commands;

public sealed class CreateCommunityChannelCommandHandler<TCreateChannelCommand>(
    ICommunityChannelsRepository repository,
    SnowflakeGenerator snowflake
) : ICommandHandler<TCreateChannelCommand, ChannelViewModel>
    where TCreateChannelCommand : CreateCommunityChannelCommand
{
    public async ValueTask<ChannelViewModel> Handle(TCreateChannelCommand command, CancellationToken cancellationToken)
    {
        CommunityChannel channel = command.Create(snowflake.Next());
        repository.Add(channel);
        await repository.UnitOfWork.SaveEntitiesAsync();
        return ChannelViewModel.From(channel);
    }
}