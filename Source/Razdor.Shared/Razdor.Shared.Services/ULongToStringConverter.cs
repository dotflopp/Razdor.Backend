using System.Text.Json;
using System.Text.Json.Serialization;

namespace Razdor.Shared.Module;

public sealed class ULongToStringConverter: JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    { 
        return Convert.ToUInt64(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}