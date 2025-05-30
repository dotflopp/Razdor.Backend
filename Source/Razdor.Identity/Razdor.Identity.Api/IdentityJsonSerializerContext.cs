using System.Text.Json.Serialization;
using Razdor.Identity.Module.Auth.Commands;
using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Api;

[JsonSerializable(typeof(LoginCommand))]
[JsonSerializable(typeof(SignupCommand))]
[JsonSerializable(typeof(AccessToken))]
[JsonSerializable(typeof(SelfUserViewModel))]
[JsonSerializable(typeof(UserPreviewModel))]
public partial class IdentityJsonSerializerContext : JsonSerializerContext
{
}