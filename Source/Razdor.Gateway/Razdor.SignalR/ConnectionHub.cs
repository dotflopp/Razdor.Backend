using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.SignalR;

public sealed partial class ConnectionHub(
    IRequestSenderContext sender    
): Hub
{
}