using System.Text.Json;
using System.Text.Json.Serialization;

namespace HyPlayer.NeteaseApi.Extensions.JsonSerializer;

public class NumberToStringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
        else if (reader.TokenType == JsonTokenType.Number && typeToConvert == typeof(string))
        {
            return reader.GetInt64().ToString();
        }
        else if (reader.TokenType == JsonTokenType.True)
        {
            return "true";
        }
        else if (reader.TokenType == JsonTokenType.False)
        {
            return "false";
        }
        else
        {
            throw new JsonException("Unexpected token type within NumberToStringConverter");
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}