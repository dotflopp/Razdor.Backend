using System.Text.Json;
using System.Text.Json.Serialization;
using Razdor.Identity.Module.Commands;

namespace Razdor.Identity.Api;

[JsonSerializable(typeof(LoginCommand))]
[JsonSerializable(typeof(SignupCommand))]
public partial class IdentityJsonSerializerContext: JsonSerializerContext
{
}