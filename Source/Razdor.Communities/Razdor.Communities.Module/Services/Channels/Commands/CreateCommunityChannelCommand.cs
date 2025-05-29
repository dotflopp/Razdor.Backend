using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Channels.ViewModels;
using Razdor.Shared.Module;

namespace Razdor.Communities.Services.Services.Channels.Commands;

public record CreateCommunityChannelCommand(
    string Name,
    [property:JsonConverter(typeof(JsonStringEnumConverter))]
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId
) : ICommunitiesCommand<ChannelViewModel>, IRequiredCommunityPermissionsMessage
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