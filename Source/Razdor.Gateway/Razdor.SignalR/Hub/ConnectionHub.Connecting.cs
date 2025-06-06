using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Communities.Queries;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Communities;

namespace Razdor.SignalR;

public partial class ConnectionHub
{
    public async override Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

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
            
            await Groups.AddToGroupAsync(Context.ConnectionId, community.Id.ToString());
        }
        
        var channels = await Task.WhenAll(getChannelTasks);

        foreach (var channel in channels.SelectMany(x => x))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channel.Id.ToString());
        }

    }
}