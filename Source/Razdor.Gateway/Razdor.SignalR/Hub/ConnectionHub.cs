using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.SignalR;

public sealed partial class ConnectionHub(
    IRequestSenderContext sender,
    ICommunitiesModule communitiesModule
): Hub<IRazdorClient>
{
    
}