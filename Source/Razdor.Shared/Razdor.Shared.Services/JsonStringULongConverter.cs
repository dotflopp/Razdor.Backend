using System.Text.Json;
using System.Text.Json.Serialization;

namespace Razdor.Shared.Module;

public sealed class JsonStringULongConverter : JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetUInt64(out ulong value))
            return value;
        return Convert.ToUInt64(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}