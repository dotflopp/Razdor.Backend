using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Module;

namespace Razdor.Api.Routes.Communities.Channels.ViewModels;

public record CommunityChannelConfiguration(
    string Name,
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId
);