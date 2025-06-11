using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.PublicEvents.ViewModels.Channels;

public record VoiceChannelViewModel(
    ulong Id,
    ulong CommunityId,
    ChannelType Type,
    ulong ParentId,
    string Name,
    bool IsSyncing,
    IEnumerable<OverwriteViewModel> Overwrites
) : ChannelViewModel(Id, CommunityId, Type, ParentId, Name, IsSyncing, Overwrites);