using System.Text.Json.Serialization;
using Razdor.Shared.Api.ViewModels;

namespace Razdor.Shared.Api;

[JsonSerializable(typeof(ExceptionViewModel))]
internal partial class SharedJsonSerializerContext : JsonSerializerContext
{
}