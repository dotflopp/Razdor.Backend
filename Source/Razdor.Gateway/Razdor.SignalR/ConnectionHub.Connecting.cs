using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Razdor.SignalR;

public partial class ConnectionHub
{
    public async override Task OnConnectedAsync()
    {
        Console.WriteLine(sender.User.Id);
        await base.OnConnectedAsync();
    }
}