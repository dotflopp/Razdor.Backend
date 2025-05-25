using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Razdor.Shared.Api.ViewModels;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Shared.Api;

[JsonSerializable(typeof(ExceptionViewModel))]
internal partial class SharedJsonSerializerContext: JsonSerializerContext
{
}