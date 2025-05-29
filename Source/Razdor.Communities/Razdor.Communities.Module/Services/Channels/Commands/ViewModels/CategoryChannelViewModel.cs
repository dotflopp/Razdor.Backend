
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Services.Services.Channels.Commands.ViewModels;

public record CategoryChannelViewModel(
    ulong Id,
    ulong CommunityId,
    ChannelType Type,
    ulong ParentId,
    bool IsSyncing,
    IEnumerable<Overwrite> Overwrites
) : ChannelViewModel(Id, CommunityId, Type, ParentId, IsSyncing, Overwrites);