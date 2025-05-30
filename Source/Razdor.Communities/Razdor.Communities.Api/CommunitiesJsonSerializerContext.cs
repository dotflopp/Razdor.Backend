using System.Text.Json.Serialization;
using Razdor.Communities.Api.Communities.Channels.ViewModels;
using Razdor.Communities.Api.Communities.Invites.ViewModels;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Services.Services.Channels.Commands;
using Razdor.Communities.Services.Services.Channels.Queries;
using Razdor.Communities.Services.Services.Channels.ViewModels;
using Razdor.Communities.Services.Services.Communities.Commands;
using Razdor.Communities.Services.Services.Communities.Queries;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Communities.Services.Services.Invites.Commands;

namespace Razdor.Communities.Api;

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