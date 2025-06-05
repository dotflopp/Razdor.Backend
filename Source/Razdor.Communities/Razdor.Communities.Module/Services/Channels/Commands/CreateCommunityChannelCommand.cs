using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public record CreateCommunityChannelCommand(
    string Name,
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId
) : ICommunitiesCommand<ChannelViewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageChannel;

    public virtual CommunityChannel Create(ulong id)
    {
        return Type switch
        {
            ChannelType.CategoryChannel => CategoryChannel.CreateNew(id, CommunityId, ParentId, Name),
            ChannelType.ForkChannel => ForkChannel.CreateNew(id, CommunityId, ParentId, Name),
            ChannelType.TextChannel => TextChannel.CreateNew(id, CommunityId, ParentId, Name),
            ChannelType.VoiceChannel => VoiceChannel.CreateNew(id, CommunityId, ParentId, Name),
            _ => throw new ArgumentException(nameof(Type), $"Unknown community type {Type}")
        };
    }
}