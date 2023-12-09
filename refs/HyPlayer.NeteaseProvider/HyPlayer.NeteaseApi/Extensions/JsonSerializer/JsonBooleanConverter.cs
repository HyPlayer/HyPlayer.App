using System.Text.Json;
using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Extensions.JsonSerializer;

public class JsonBooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return !(reader.GetString() == "false" || string.IsNullOrWhiteSpace(reader.GetString()));
            case JsonTokenType.Number:
                return reader.GetInt32() > 0;
                break;
            case JsonTokenType.None:
            case JsonTokenType.False:
            case JsonTokenType.Null:
                return false;
            case JsonTokenType.True:
                return true;
            default:
                throw new JsonException("Unexpected token type within BooleanConverter");
        }
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}