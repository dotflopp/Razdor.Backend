using Mediator;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Communities.Queries;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.SignalR.Services;

public class AcceptConnectionCommandHandler(
    IHubContext<ConnectionHub, IRazdorClient> context,
    IRequestSenderContext sender,
    ICommunitiesModule communitiesModule
): ICommandHandler<AcceptConnectionCommand>
{

    public async ValueTask<Unit> Handle(AcceptConnectionCommand command, CancellationToken cancellationToken)
    {
        IGroupManager groups = context.Groups;
        string connectionId = command.ConnectionId;
        
        await groups.AddToGroupAsync(connectionId, sender.User.Id.ToString());
        
        IEnumerable<CommunityViewModel> communities = await communitiesModule.ExecuteQueryAsync(
            new GetSelfUserCommunitiesQuery()
        );
        
        var getChannelTasks = new List<Task<IEnumerable<ChannelViewModel>>>();
        
        foreach (var community in communities)
        {
            var getChannelTask = communitiesModule.ExecuteQueryAsync(
                new GetCommunityChannelsQuery(community.Id)
            );
            getChannelTasks.Add(getChannelTask);
            
            await groups.AddToGroupAsync(connectionId, community.Id.ToString());
        }
        
        var channels = await Task.WhenAll(getChannelTasks);

        foreach (var channel in channels.SelectMany(x => x))
        {
            await groups.AddToGroupAsync(connectionId, channel.Id.ToString());
        }
        
        return Unit.Value;
    }
}