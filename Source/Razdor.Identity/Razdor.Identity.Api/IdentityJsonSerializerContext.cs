using System.Text.Json;
using System.Text.Json.Serialization;
using Razdor.Identity.Module.Commands;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Api;

[JsonSerializable(typeof(LoginCommand))]
[JsonSerializable(typeof(SignupCommand))]
[JsonSerializable(typeof(AccessTokenViewModel))]
[JsonSerializable(typeof(AuthenticationError))]
public partial class IdentityJsonSerializerContext: JsonSerializerContext
{
}