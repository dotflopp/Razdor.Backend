using System.Text.Json.Serialization;
using Razdor.RestApi.ExceptionHandleMiddlewares.ViewModels;

namespace Razdor.RestApi.Serialization;

[JsonSerializable(typeof(ExceptionViewModel))]
public partial class SharedJsonSerializerContext : JsonSerializerContext
{
}