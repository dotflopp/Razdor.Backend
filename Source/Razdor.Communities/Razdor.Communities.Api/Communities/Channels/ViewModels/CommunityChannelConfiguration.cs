using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Shared.Module;

namespace Razdor.Communities.Api.Communities.Channels.ViewModels;

public record CommunityChannelConfiguration(
    string Name,
    [property:JsonConverter(typeof(JsonStringEnumConverter))]
    ChannelType Type, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId
);