using Razdor.Communities.Api.Communities.Channels.ViewModels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Services.Services.Channels.Commands.ViewModels;

public record TextChannelViewModel(
    ulong Id, 
    ulong CommunityId, 
    ChannelType Type, 
    ulong ParentId, 
    string Name,
    bool IsSyncing, 
    IEnumerable<OverwriteViewModel> Overwrites
) : ChannelViewModel(Id, CommunityId, Type, ParentId, Name, IsSyncing, Overwrites);