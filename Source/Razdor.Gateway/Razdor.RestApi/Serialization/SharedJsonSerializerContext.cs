using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Razdor.RestApi.ExceptionHandleMiddlewares.ViewModels;

namespace Razdor.RestApi.Serialization;

[JsonSerializable(typeof(ExceptionViewModel))]
[JsonSerializable(typeof(IFormFile))]
[JsonSerializable(typeof(FileContentResult))]
[JsonSerializable(typeof(UInt64?))]
[JsonSerializable(typeof(UInt32?))]
[JsonSerializable(typeof(Int32?))]
public partial class SharedJsonSerializerContext : JsonSerializerContext
{
}