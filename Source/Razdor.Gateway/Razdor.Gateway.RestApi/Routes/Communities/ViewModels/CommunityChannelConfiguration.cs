using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Module.Serialization;

namespace Razdor.RestApi.Routes.Communities.ViewModels;

public record CommunityChannelConfiguration(
    string Name,
    ChannelType Type,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ParentId
);