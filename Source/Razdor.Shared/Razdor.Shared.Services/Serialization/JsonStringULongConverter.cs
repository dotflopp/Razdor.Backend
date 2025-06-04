using System.Text.Json;
using System.Text.Json.Serialization;

namespace Razdor.Shared.Module;

public sealed class JsonStringULongConverter : JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            if (reader.TokenType == JsonTokenType.Null)
                return 0;
            
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetUInt64(out ulong value))
                return value;
            
            return Convert.ToUInt64(reader.GetString());
        }
        catch (FormatException exception)
        {
            throw new JsonException(exception.Message, exception);
        }
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}