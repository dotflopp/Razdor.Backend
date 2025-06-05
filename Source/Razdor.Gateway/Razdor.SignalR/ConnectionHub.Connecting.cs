using Microsoft.AspNetCore.Authorization;

namespace Razdor.SignalR;

public partial class ConnectionHub
{
    public async override Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}