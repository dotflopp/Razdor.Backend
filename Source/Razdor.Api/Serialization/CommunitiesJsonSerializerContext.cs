using System.Text.Json.Serialization;
using Razdor.Api.Routes.Communities.Channels.ViewModels;
using Razdor.Api.Routes.Communities.Invites.ViewModels;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.Communities.Module.Services.Communities.Commands;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Communities.Module.Services.Invites.Commands;

namespace Razdor.Api.Serialization;

[JsonSerializable(typeof(InviteParametersViewModel))]
[JsonSerializable(typeof(CommunityChannelConfiguration))]
[JsonSerializable(typeof(CreateCommunityChannelCommand))]
[JsonSerializable(typeof(GetCommunityChannelsQuery))]
[JsonSerializable(typeof(CategoryChannelViewModel))]
[JsonSerializable(typeof(ChannelViewModel))]
[JsonSerializable(typeof(ForkChannelViewModel))]
[JsonSerializable(typeof(OverwriteViewModel))]
[JsonSerializable(typeof(TextChannelViewModel))]
[JsonSerializable(typeof(VoiceChannelViewModel))]
[JsonSerializable(typeof(CreateCommunityChannelCommand))]
[JsonSerializable(typeof(CreateCommunityCommand))]
[JsonSerializable(typeof(GetCommunityChannelsQuery))]
[JsonSerializable(typeof(CommunityViewModel))]
[JsonSerializable(typeof(InviteViewModel))]
[JsonSerializable(typeof(RoleViewModel))]
[JsonSerializable(typeof(AcceptInviteCommand))]
[JsonSerializable(typeof(CreateInviteCommand))]
[JsonSerializable(typeof(IEnumerable<CommunityViewModel>))]
[JsonSerializable(typeof(IEnumerable<ChannelViewModel>))]
[JsonSerializable(typeof(SessionViewModel))]
[JsonSerializable(typeof(ConnectChannelCommand))]
public partial class CommunitiesJsonSerializerContext: JsonSerializerContext
{
    
}