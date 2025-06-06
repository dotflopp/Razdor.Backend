using System.Text.Json.Serialization;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Communities.Commands;
using Razdor.Communities.Module.Services.Invites.Commands;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Communities.PublicEvents.ViewModels.Invites;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.RestApi.Routes.Channels.Overwrites.ViewModels;
using Razdor.RestApi.Routes.Communities.Roles.ViewModels;
using Razdor.RestApi.Routes.Communities.ViewModels;
using Razdor.RestApi.Routes.Invites.ViewModels;

namespace Razdor.RestApi.Serialization;

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
[JsonSerializable(typeof(InvitePreviewModel))]
[JsonSerializable(typeof(RoleViewModel))]
[JsonSerializable(typeof(AcceptInviteCommand))]
[JsonSerializable(typeof(CreateInviteCommand))]
[JsonSerializable(typeof(IEnumerable<CommunityViewModel>))]
[JsonSerializable(typeof(IEnumerable<ChannelViewModel>))]
[JsonSerializable(typeof(SessionViewModel))]
[JsonSerializable(typeof(ConnectChannelCommand))]
[JsonSerializable(typeof(OverwritePyload))]
[JsonSerializable(typeof(RolePyload))]
[JsonSerializable(typeof(InviteViewModel))]
[JsonSerializable(typeof(IEnumerable<CommunityMemberPreviewModel>))]
public partial class CommunitiesJsonSerializerContext: JsonSerializerContext
{
    
}