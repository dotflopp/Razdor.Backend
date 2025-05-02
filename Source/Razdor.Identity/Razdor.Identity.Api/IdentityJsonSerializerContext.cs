using System.Text.Json.Serialization;
using Razdor.Identity.Module.Auth.Commands;
using Razdor.Identity.Module.Auth.Commands.ViewModels;

namespace Razdor.Identity.Api;

[JsonSerializable(typeof(LoginCommand))]
[JsonSerializable(typeof(SignupCommand))]
[JsonSerializable(typeof(AccessToken))]
[JsonSerializable(typeof(AuthenticationError))]
public partial class IdentityJsonSerializerContext : JsonSerializerContext
{
}