using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Razdor.Shared.Extensions;

public static class JsonSerializerContextExtensions
{
    public static JsonTypeInfo GetRequiredTypeInfo<T>(this JsonSerializerContext context)
    {
        return context.GetTypeInfo(typeof(T))
               ?? throw new InvalidOperationException($"Required type {nameof(T)} is not resolved");
    }
}