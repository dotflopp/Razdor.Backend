using System.Text.Json.Serialization;
using Razdor.Api.ExceptionHandleMiddlewares.ViewModels;

namespace Razdor.Api.Serialization;

[JsonSerializable(typeof(ExceptionViewModel))]
public partial class SharedJsonSerializerContext : JsonSerializerContext
{
}